using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonController : MonoBehaviour
{
    private enum ActionMode
    {
        Patrol,
        Aggressive,
        Rest,
        Search
    }

    private enum MovementMode
    {
        Ground,
        Air
    }

    [SerializeField]
    private Transform Target;
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private float MeleeAttackRange, RangeAttackRange, MeleeAttackInterval, RangeAttackInterval,
        ShoutInterval,
        PatrolRange, PatrolSpeed, PatrolTime,
        RestTime,
        SpotRange, SpotAngle,
        PursuitSpeed,
        BaseAcceleration,
        SearchRange, SearchTime, SearchSpeed,
        AirSpeedFactor, AirAccelerationFactor, AirTime,
        HealthPointThreshold,
        AngularSpeed,
        MeleeStopDistance, RangeStopDistance;
    [SerializeField]
    private GameObject Shoutwave;

    protected const string MovementAnimationName = "Movement",
        Attack1AnimationName = "Attack 1",
        Attack2AnimationName = "Attack 2",
        ScreamAnimationName = "Scream",
        TakeOffAnimationName = "Take Off",
        LandAnimationName = "Land";

    private float nextAttack = 0f,
        nextScream = 0f,
        restTimeFinishAt = 0f,
        patrolTimeFinishAt = 0f,
        searchTimeFinishedAt = 0f,
        airTimeFinishedAt = 0f;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private ActionMode actionMode;
    private MovementMode movementMode;
    private Health myHealth;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        actionMode = ActionMode.Rest;
        movementMode = MovementMode.Ground;
        myHealth = GetComponent<Health>();
        myHealth.OnHurt += MyHealth_OnHurt;
        //Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void MyHealth_OnHurt(object sender, Vector3 direction)
    {
        if (!IsHealthy()) { Land(); }
        if (actionMode != ActionMode.Aggressive)
        {
            searchTimeFinishedAt = Time.time + SearchTime;
            actionMode = ActionMode.Search;
            Search(direction);
        }
    }
    private void TakeOff()
    {
        airTimeFinishedAt = Time.time + AirTime;
        myAnimator.SetTrigger(TakeOffAnimationName);
        movementMode = MovementMode.Air;
    }
    private void Land()
    {
        airTimeFinishedAt = Time.time;
        myAnimator.SetTrigger(LandAnimationName);
        movementMode = MovementMode.Ground;
    }
    private void Pursuit()
    {
        switch (movementMode)
        {
            case MovementMode.Ground:
                agent.acceleration = BaseAcceleration;
                agent.speed = PursuitSpeed;
                agent.stoppingDistance = MeleeStopDistance;
                break;
            case MovementMode.Air:
                agent.acceleration = BaseAcceleration * AirAccelerationFactor;
                agent.speed = PursuitSpeed * AirSpeedFactor;
                agent.stoppingDistance = RangeStopDistance;
                airTimeFinishedAt = Time.time + AirTime;
                break;
        }
        agent.destination = Target.position;
    }
    private void MeleeAttack()
    {
        myAnimator.ResetTrigger(ScreamAnimationName);
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + MeleeAttackInterval;
            myAnimator.SetTrigger(Attack1AnimationName);
        }
    }
    private void RangeAttack()
    {
        myAnimator.ResetTrigger(ScreamAnimationName);
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + RangeAttackInterval;
            myAnimator.SetTrigger(Attack2AnimationName);
        }
    }
    private void Scream()
    {
        myAnimator.ResetTrigger(Attack1AnimationName);
        myAnimator.ResetTrigger(Attack2AnimationName);
        if (Time.time >= nextScream)
        {
            nextScream = Time.time + ShoutInterval;
            myAnimator.SetTrigger(ScreamAnimationName);
            Instantiate(Shoutwave, transform.position, transform.rotation);
        }
    }
    private void Patrol()
    {
        patrolTimeFinishAt = Time.time + PatrolTime;
        actionMode = ActionMode.Patrol;
        agent.stoppingDistance = 0;
        Vector3 direction = new Vector3(Random.Range(-PatrolRange, PatrolRange),
            Random.Range(-PatrolRange, PatrolRange),
            Random.Range(-PatrolRange, PatrolRange));
        switch (movementMode)
        {
            case MovementMode.Ground:
                agent.speed = PatrolSpeed;
                agent.acceleration = BaseAcceleration;
                break;
            case MovementMode.Air:
                agent.speed = PatrolSpeed * AirSpeedFactor;
                agent.acceleration = BaseAcceleration * AirAccelerationFactor;
                break;
        }
        agent.destination = transform.position + direction;
    }
    private void Rest()
    {
        restTimeFinishAt = Time.time + RestTime;
        actionMode = ActionMode.Rest;
        agent.destination = transform.position;
        agent.stoppingDistance = 0;
    }
    private void Search(Vector3 direction)
    {
        switch (movementMode)
        {
            case MovementMode.Ground:
                agent.speed = SearchSpeed;
                agent.acceleration = BaseAcceleration;
                break;
            case MovementMode.Air:
                agent.speed = SearchSpeed * AirSpeedFactor;
                agent.acceleration = BaseAcceleration * AirAccelerationFactor;
                break;
        }
        agent.stoppingDistance = 0;
        agent.destination = transform.position + (direction * SearchRange);
    }
    private bool IsEnemyWithinSpotingDistance()
    {
        return Vector3.Distance(transform.position, Target.position) <= SpotRange;
    }
    private bool IsEnemyWithinSpotAngle()
    {
        return Vector3.Angle(transform.forward, Target.position - transform.position) <= SpotAngle;
    }
    private bool IsEnemyInSight()
    {
        bool isHit = Physics.Raycast(new Ray(transform.position, Target.position - transform.position), out RaycastHit hit, SpotRange);
        //Debug.DrawLine(transform.position, hit.point, Color.red);
        if (isHit)
        {
            return EnemyMask == (EnemyMask | (1 << hit.transform.gameObject.layer));
        }
        else
        {
            return false;
        }
    }
    private bool IsPatrolTimeFinished()
    {
        return Time.time >= patrolTimeFinishAt;
    }
    private bool IsRestTimeFinished()
    {
        return Time.time >= restTimeFinishAt;
    }
    private bool IsSearchTimeFinished()
    {
        return Time.time >= searchTimeFinishedAt;
    }
    private bool IsAirTimeFinished()
    {
        return Time.time >= airTimeFinishedAt;
    }
    private bool IsHealthy()
    {
        return myHealth.HealthPoint() >= HealthPointThreshold;
    }
    private bool IsGroundMode()
    {
        return movementMode == MovementMode.Ground;
    }
    private bool IsAirMode()
    {
        return movementMode == MovementMode.Air;
    }
    private void UpdateMovementAnimation()
    {
        switch (movementMode)
        {
            case MovementMode.Ground:
                myAnimator.SetFloat(MovementAnimationName, agent.velocity.magnitude / PursuitSpeed);
                break;
            case MovementMode.Air:
                myAnimator.SetFloat(MovementAnimationName, agent.velocity.magnitude / (PursuitSpeed * AirSpeedFactor));
                break;
        }
    }
    private void FaceTarget()
    {
        Vector3 lookPos = Target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, AngularSpeed);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(movementMode + " " + actionMode);
            Debug.Log(agent.speed + " " + agent.acceleration + " " + agent.stoppingDistance);
            Debug.Log(Vector3.Distance(Target.position, transform.position));

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            myHealth.HealthChange(-10, Target.position - transform.position);
        }
        bool isHit = Physics.Raycast(new Ray(transform.position, Target.position - transform.position), out RaycastHit hit, SpotRange);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (myHealth.IsAlive)
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
            switch (actionMode)
            {
                case ActionMode.Patrol:
                    if (IsEnemyWithinSpotingDistance()
                        && IsEnemyWithinSpotAngle()
                        && IsEnemyInSight())
                    {
                        actionMode = ActionMode.Aggressive;
                        if (IsHealthy() && IsGroundMode())
                        { TakeOff(); }
                        return;
                    }
                    if (IsPatrolTimeFinished()) { Rest(); }
                    break;
                case ActionMode.Aggressive:
                    if (!IsEnemyWithinSpotingDistance()) { Rest(); }
                    Pursuit();
                    switch (movementMode)
                    {
                        case MovementMode.Ground:
                            if (Vector3.Distance(Target.position, transform.position) <= MeleeAttackRange) { FaceTarget(); MeleeAttack(); }
                            else
                            {
                                Scream();
                                if (Vector3.Distance(Target.position, transform.position) > RangeAttackRange && IsHealthy()) { TakeOff(); }
                            }
                            break;
                        case MovementMode.Air:
                            if (Vector3.Distance(Target.position, transform.position) <= MeleeAttackRange)
                            { Land(); }
                            else if (Vector3.Distance(Target.position, transform.position) <= RangeAttackRange) { FaceTarget(); RangeAttack(); }
                            else { Scream(); }
                            break;
                    }
                    break;
                case ActionMode.Rest:
                    if (IsEnemyWithinSpotingDistance()
                        && IsEnemyWithinSpotAngle()
                        && IsEnemyInSight())
                    {
                        actionMode = ActionMode.Aggressive;
                        if (IsHealthy() && IsGroundMode())
                        { TakeOff(); }
                        return;
                    }
                    if (IsAirTimeFinished() && IsAirMode())
                    { Land(); }
                    if (IsRestTimeFinished()) { Patrol(); }
                    break;
                case ActionMode.Search:
                    if (IsEnemyWithinSpotingDistance()
                        && IsEnemyWithinSpotAngle()
                        && IsEnemyInSight())
                    {
                        actionMode = ActionMode.Aggressive;
                        if (IsHealthy() && IsGroundMode())
                        { TakeOff(); }
                        return;
                    }
                    if (IsSearchTimeFinished()) { Rest(); }
                    break;
            }
            UpdateMovementAnimation();

        }
        else
        {
            agent.destination = transform.position;
        }
    }
}
