using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RhinoController : MonoBehaviour
{
    private enum ActionMode
    {
        Patrol,
        Flee,
        Rest
    }
    [SerializeField]
    private GameObject WizardPrefab,
        SmokeEmitter;
    [SerializeField]
    private float PatrolRange = 5, PatrolSpeed = 2, PatrolTime = 5,
        RestTime = 5,
        FleeSpeed = 2.5f, FleeRange = 2, FleeTime = 3,
        DeathToSmokeInterval = 3, SmokeEmitterLifeTime = 3,
        SmokeToWizardInterval = 2;

    protected const string 
        animationParameter_Movement = "Movement",
        animationParameter_Hurt = "Hurt", 
        animationParameter_Dead = "Dead";

    private float restTimeFinishAt = 0f,
        patrolTimeFinishAt = 0f,
        fleeTimeFinishedAt;

    private bool wizardSpawned = false;

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
        agent.avoidancePriority = 99;
    }

    private void MyHealth_OnHurt(object sender, Vector3 direction)
    {
        if (actionMode != ActionMode.Flee)
        {
            actionMode = ActionMode.Flee;
            Flee(-direction);
        }
        myAnimator.SetTrigger(animationParameter_Hurt);
    }
    private void MyHealth_OnDead(object sender, System.EventArgs e)
    {
        myAnimator.SetFloat(animationParameter_Movement, 0);
        myAnimator.SetTrigger(animationParameter_Dead);
        IEnumerator coroutine = Dead();
        StartCoroutine(coroutine);
    }

    private IEnumerator Dead()
    {
        if (!wizardSpawned)
        {
            wizardSpawned = true;
            yield return new WaitForSeconds(DeathToSmokeInterval);
            GameObject smokeEmitter = Instantiate(SmokeEmitter, transform.position, transform.rotation);
            Destroy(smokeEmitter, SmokeEmitterLifeTime);
            yield return new WaitForSeconds(SmokeToWizardInterval);
            Instantiate(WizardPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
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
    private void Flee(Vector3 direction)
    {
        agent.speed = FleeSpeed;
        agent.destination = transform.position + (direction * FleeRange);
        fleeTimeFinishedAt = Time.time + FleeTime;
        /*Debug.DrawRay(agent.destination, Vector3.up * 100, Color.green, 3600);
        Debug.DrawRay(transform.position, (direction * SearchRange), Color.red, 5);*/
    }
    private bool IsPatrolTimeFinished()
    {
        return Time.time >= patrolTimeFinishAt;
    }
    private bool IsRestTimeFinished()
    {
        return Time.time >= restTimeFinishAt;
    }
    private bool IsFleeTimeFinished()
    {
        return Time.time >= fleeTimeFinishedAt;
    }
    private void UpdateMovementAnimation()
    {
        myAnimator.SetFloat(animationParameter_Movement, agent.velocity.magnitude / FleeSpeed);
    }
    private void Update()
    {
        if (myHealth.IsAlive)
        {
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);
            switch (actionMode)
            {
                case ActionMode.Patrol:
                    if (IsPatrolTimeFinished())
                    {
                        restTimeFinishAt = Time.time + RestTime;
                        actionMode = ActionMode.Rest;
                        Rest();
                    }
                    break;
                case ActionMode.Rest:
                    if (IsRestTimeFinished())
                    {
                        patrolTimeFinishAt = Time.time + PatrolTime;
                        actionMode = ActionMode.Patrol;
                        Patrol();
                    }
                    break;
                case ActionMode.Flee:
                    if (IsFleeTimeFinished())
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
