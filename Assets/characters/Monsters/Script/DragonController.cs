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
    private LayerMask EnemyMask;
    [SerializeField]
    private GameObject SmokeEmitterPrefab;
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
        MeleeStopDistance, RangeStopDistance,
        SmokeEmitterLifeTime = 2, DeathToSmokeInterval = 10, SmokeToCleatCorpseInterval = 1;

    protected const string 
        animationParameter_Movement = "Movement",
        animationParameter_Attack_1 = "Attack 1",
        animationParameter_Attack_2 = "Attack 2",
        animationParameter_Scream = "Scream",
        animationParameter_TakeOff = "Take Off",
        animationParameter_Land = "Land",
        animationParameter_Hurt = "Hurt", 
        animationParameter_Dead = "Dead";

    private float nextAttack = 0f,
        nextScream = 0f,
        restTimeFinishAt = 0f,
        patrolTimeFinishAt = 0f,
        searchTimeFinishedAt = 0f,
        airTimeFinishedAt = 0f;

    private Transform target;
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
        myHealth.OnDead += MyHealth_OnDead;
    }


    private void Start()
    {
        target = Players.CurrentPlayer.transform;
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
        myAnimator.SetTrigger(animationParameter_Hurt);
    }
    private void MyHealth_OnDead(object sender, System.EventArgs e)
    {
        myAnimator.SetFloat(animationParameter_Movement, 0);
        myAnimator.SetTrigger(animationParameter_Dead);
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(DeathToSmokeInterval);
        GameObject smokeEmitter = Instantiate(SmokeEmitterPrefab, transform.position, transform.rotation);
        Destroy(smokeEmitter, SmokeEmitterLifeTime);
        yield return new WaitForSeconds(SmokeToCleatCorpseInterval);
        Destroy(gameObject);
    }
    private void TakeOff()
    {
        airTimeFinishedAt = Time.time + AirTime;
        myAnimator.SetTrigger(animationParameter_TakeOff);
        movementMode = MovementMode.Air;
    }
    private void Land()
    {
        airTimeFinishedAt = Time.time;
        myAnimator.SetTrigger(animationParameter_Land);
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
        agent.destination = target.position;
    }
    private void MeleeAttack()
    {
        myAnimator.ResetTrigger(animationParameter_Scream);
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + MeleeAttackInterval;
            myAnimator.SetTrigger(animationParameter_Attack_1);
        }
    }
    private void RangeAttack()
    {
        myAnimator.ResetTrigger(animationParameter_Scream);
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + RangeAttackInterval;
            myAnimator.SetTrigger(animationParameter_Attack_2);
        }
    }
    private void Scream()
    {
        myAnimator.ResetTrigger(animationParameter_Attack_1);
        myAnimator.ResetTrigger(animationParameter_Attack_2);
        if (Time.time >= nextScream)
        {
            nextScream = Time.time + ShoutInterval;
            myAnimator.SetTrigger(animationParameter_Scream);
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
        return Vector3.Distance(transform.position, target.position) <= SpotRange;
    }
    private bool IsEnemyWithinSpotAngle()
    {
        return Vector3.Angle(transform.forward, target.position - transform.position) <= SpotAngle;
    }
    private bool IsEnemyInSight()
    {
        bool isHit = Physics.Raycast(new Ray(transform.position, target.position - transform.position), out RaycastHit hit, SpotRange);
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
                myAnimator.SetFloat(animationParameter_Movement, agent.velocity.magnitude / PursuitSpeed);
                break;
            case MovementMode.Air:
                myAnimator.SetFloat(animationParameter_Movement, agent.velocity.magnitude / (PursuitSpeed * AirSpeedFactor));
                break;
        }
    }
    private void FaceTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, AngularSpeed * Time.deltaTime);
    }
    private void Update()
    {
        bool isHit = Physics.Raycast(new Ray(transform.position, target.position - transform.position), out RaycastHit hit, SpotRange);
        if (myHealth.IsAlive)
        {
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
                            if (Vector3.Distance(target.position, transform.position) <= MeleeAttackRange) { FaceTarget(); MeleeAttack(); }
                            else
                            {
                                Scream();
                                if (Vector3.Distance(target.position, transform.position) > RangeAttackRange && IsHealthy()) { TakeOff(); }
                            }
                            break;
                        case MovementMode.Air:
                            if (Vector3.Distance(target.position, transform.position) <= MeleeAttackRange)
                            { Land(); }
                            else if (Vector3.Distance(target.position, transform.position) <= RangeAttackRange) { FaceTarget(); RangeAttack(); }
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
