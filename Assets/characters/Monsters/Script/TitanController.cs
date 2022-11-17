using UnityEngine;
using UnityEngine.AI;


public class TitanController :MonoBehaviour, IEnemyController
{
    private enum ActionMode
    {
        Patrol,
        Aggressive,
        Rest,
        Search
    }

    [SerializeField]
    private Transform Target;
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private float AttackRange, AttackInterval,
        ShoutInterval,
        PatrolRange, PatrolSpeed, PatrolTime,
        RestTime,
        SpotRange, SpotAngle,
        PursuitSpeed,
        SearchRange, SearchTime, SearchSpeed;
    [SerializeField]
    private GameObject Shoutwave;

    protected const string MovementAnimationName = "Movement",
        Attack1AnimationName = "Attack 1",
        Attack2AnimationName = "Attack 2",
        Shout1AnimationName = "Shout 1",
        Shout2AnimationName = "Shout 2";

    private float nextAttack = 0f,
        nextShout = 0f,
        restTimeFinishAt = 0f,
        patrolTimeFinishAt = 0f,
        searchTimeFinishedAt = 0f;

    private NavMeshAgent agent;
    private Animator myAnimator;
    private ActionMode actionMode;
    private Health myHealth;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        actionMode = ActionMode.Rest;
        myHealth = GetComponent<Health>();
        myHealth.OnHurt += MyHealth_OnHurt;
        //Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void MyHealth_OnHurt(object sender, Vector3 direction)
    {
        if (actionMode != ActionMode.Aggressive)
        {
            searchTimeFinishedAt = Time.time + SearchTime;
            actionMode = ActionMode.Search;
            Search(direction);
        }
    }
    private void Pursuit()
    {
        agent.speed = PursuitSpeed;
        agent.destination = Target.position;
    }
    private void Attack()
    {
        myAnimator.ResetTrigger(Shout1AnimationName);
        myAnimator.ResetTrigger(Shout2AnimationName);
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + AttackInterval;
            switch (Random.Range(0, 2))
            {
                case 0:
                    myAnimator.SetTrigger(Attack1AnimationName);
                    break;
                case 1:
                    myAnimator.SetTrigger(Attack2AnimationName);
                    break;
            }
        }
    }
    private void Shout()
    {
        myAnimator.ResetTrigger(Attack1AnimationName);
        myAnimator.ResetTrigger(Attack2AnimationName);
        if (Time.time >= nextShout)
        {
            nextShout = Time.time + ShoutInterval;
            switch (Random.Range(0, 2))
            {
                case 0:
                    myAnimator.SetTrigger(Shout1AnimationName);
                    break;
                case 1:
                    myAnimator.SetTrigger(Shout2AnimationName);
                    break;
            }
            Instantiate(Shoutwave, transform.position, transform.rotation);
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
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (isHit)
        {
            Debug.Log(hit.transform.gameObject.name);
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

    private void UpdateMovementAnimation()
    {
        myAnimator.SetFloat(MovementAnimationName, agent.velocity.magnitude / PursuitSpeed);
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
                    if (Vector3.Distance(Target.position, transform.position) <= AttackRange)
                    {
                        Attack();
                    }
                    else
                    {
                        Shout();
                    }
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
        {
            agent.destination = transform.position;
        }
    }
}
