using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TitanController : MonoBehaviour
{
    private enum ActionMode
    {
        Patrol,
        Aggressive,
        Rest,
        Search
    }

    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private GameObject SmokeEmitterPrefab;
    [SerializeField]
    private float AttackRange, AttackInterval,
        ShoutInterval,
        PatrolRange, PatrolSpeed, PatrolTime,
        RestTime,
        SpotRange, SpotAngle,
        PursuitSpeed,
        SearchRange, SearchTime, SearchSpeed,
        AngularSpeed,
        SmokeEmitterLifeTime = 2, DeathToSmokeInterval = 10, SmokeToCleatCorpseInterval = 1;

    protected const string 
        animationParameter_Movement = "Movement",
        animationParameter_Attack_1 = "Attack 1",
        animationParameter_Attack_2 = "Attack 2",
        animationParameter_Shout_1 = "Shout 1",
        animationParameter_Shout_2 = "Shout 2",
        animationParameter_Hurt = "Hurt", 
        animationParameter_Dead = "Dead";

    private float nextAttack = 0f,
        nextShout = 0f,
        restTimeFinishAt = 0f,
        patrolTimeFinishAt = 0f,
        searchTimeFinishedAt = 0f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator myAnimator;
    private ActionMode actionMode;
    private Health myHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        Rest();
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
        if (actionMode != ActionMode.Aggressive)
        {
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
    private void Pursuit()
    {
        agent.speed = PursuitSpeed;
        agent.destination = target.position;
    }
    private void Attack()
    {
        myAnimator.ResetTrigger(animationParameter_Shout_1);
        myAnimator.ResetTrigger(animationParameter_Shout_2);
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + AttackInterval;
            switch (Random.Range(0, 2))
            {
                case 0:
                    myAnimator.SetTrigger(animationParameter_Attack_1);
                    break;
                case 1:
                    myAnimator.SetTrigger(animationParameter_Attack_2);
                    break;
            }
        }
    }
    private void Shout()
    {
        myAnimator.ResetTrigger(animationParameter_Attack_1);
        myAnimator.ResetTrigger(animationParameter_Attack_2);
        if (Time.time >= nextShout)
        {
            nextShout = Time.time + ShoutInterval;
            switch (Random.Range(0, 2))
            {
                case 0:
                    myAnimator.SetTrigger(animationParameter_Shout_1);
                    break;
                case 1:
                    myAnimator.SetTrigger(animationParameter_Shout_2);
                    break;
            }
        }
    }
    private void Patrol()
    {
        Vector3 direction = new Vector3(Random.Range(-PatrolRange, PatrolRange),
            Random.Range(-PatrolRange, PatrolRange),
            Random.Range(-PatrolRange, PatrolRange));
        agent.speed = PatrolSpeed;
        agent.destination = transform.position + direction;
    }
    private void Rest()
    {
        agent.destination = transform.position;
    }
    private void Search(Vector3 direction)
    {
        agent.speed = SearchSpeed;
        agent.destination = transform.position + (direction * SearchRange);
        searchTimeFinishedAt = Time.time + SearchTime;
        /*Debug.DrawRay(agent.destination, Vector3.up * 100, Color.green, 3600);
        Debug.DrawRay(transform.position, (direction * SearchRange), Color.red, 5);*/
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
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (isHit)
        { return EnemyMask == (EnemyMask | (1 << hit.transform.gameObject.layer)); }
        else
        { return false; }
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
    private void UpdateMovementAnimation()
    {
        myAnimator.SetFloat(animationParameter_Movement, agent.velocity.magnitude / PursuitSpeed);
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
        if (myHealth.IsAlive)
        {
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
            switch (actionMode)
            {
                case ActionMode.Patrol:
                    if (IsEnemyWithinSpotingDistance()
                        && IsEnemyWithinSpotAngle()
                        && IsEnemyInSight())
                    {
                        actionMode = ActionMode.Aggressive;
                        return;
                    }
                    if (IsPatrolTimeFinished())
                    {
                        restTimeFinishAt = Time.time + RestTime;
                        actionMode = ActionMode.Rest;
                        Rest();
                    }
                    break;
                case ActionMode.Aggressive:
                    if (!IsEnemyWithinSpotingDistance())
                    {
                        restTimeFinishAt = Time.time + RestTime;
                        actionMode = ActionMode.Rest;
                        Rest();
                    }
                    Pursuit();
                    if (Vector3.Distance(target.position, transform.position) <= AttackRange)
                    { FaceTarget(); Attack(); }
                    else { Shout(); }
                    break;
                case ActionMode.Rest:
                    if (IsEnemyWithinSpotingDistance()
                        && IsEnemyWithinSpotAngle()
                        && IsEnemyInSight())
                    {
                        actionMode = ActionMode.Aggressive;
                        return;
                    }
                    if (IsRestTimeFinished())
                    {
                        patrolTimeFinishAt = Time.time + PatrolTime;
                        actionMode = ActionMode.Patrol;
                        Patrol();
                    }
                    break;
                case ActionMode.Search:
                    if (IsEnemyWithinSpotingDistance()
                        && IsEnemyWithinSpotAngle()
                        && IsEnemyInSight())
                    {
                        actionMode = ActionMode.Aggressive;
                        return;
                    }
                    if (IsSearchTimeFinished())
                    {
                        restTimeFinishAt = Time.time + RestTime;
                        actionMode = ActionMode.Rest;
                        Rest();
                    }
                    break;
            }
            UpdateMovementAnimation();

        }
        else
        { agent.destination = transform.position; }
    }
}
