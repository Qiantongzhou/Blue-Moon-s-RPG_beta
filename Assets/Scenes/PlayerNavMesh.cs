using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    private enum ActionMode
    {
        Patrol,
        Aggressive,
        Rest
    }
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private float AttackRange, AttackInterval,
        PatrolRange, PatrolSpeed, PatrolTime,
        RestTime,
        SpotRange, SpotAngle,
        PursuitSpeed;
    private float nextAttack = 0f, restTimeFinishAt = 0f, patrolTimeFinishAt = 0f;
   

    private NavMeshAgent agent;
    private Animator myAnimator;
    private ActionMode actionMode;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        actionMode = ActionMode.Rest;
    }
    private void Pursuit()
    {
        agent.speed = PursuitSpeed;
        agent.destination = Target.position;
    }
    private void Attack()
    {
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + AttackInterval;
            if (Vector3.Distance(transform.position, agent.destination) <= AttackRange)
            {
                int i = Random.Range(0, 2);
                Debug.Log(i);
                switch (i)
                {
                    case 0:
                        myAnimator.SetTrigger("Attack 1");
                        break;
                    case 1:
                        myAnimator.SetTrigger("Attack 2");
                        break;
                }
            }
            else
            {
                myAnimator.ResetTrigger("Attack 1");
                myAnimator.ResetTrigger("Attack 2");
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
    private void UpdateMovementAnimation()
    {
        myAnimator.SetFloat("Movement", agent.velocity.magnitude / PursuitSpeed);
    }
    private void Update()
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
                    actionMode = ActionMode.Patrol;
                    return;
                }
                Pursuit();
                Attack();
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
        }
        UpdateMovementAnimation();
    }
}
