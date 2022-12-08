using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PainController : MonoBehaviour
{
    [SerializeField]
    private GameObject SmokeEmitterPrefab;
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private float AttackRange = 2, AttackInterval = 3,
        PursuitSpeed = 3,
        AngularSpeed = 1,
        SmokeEmitterLifeTime = 2, DeathToSmokeInterval = 3, SmokeToCleatCorpseInterval = 1;

    protected const string MovementAnimationName = "Movement",
        Attack1AnimationName = "Attack";

    private float nextAttack = 0f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator myAnimator;
    private Health myHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        myHealth = GetComponent<Health>();
        myHealth.OnDead += MyHealth_OnDead;

        GameObject smokeEmitter = Instantiate(SmokeEmitterPrefab, transform.position, transform.rotation);
        Destroy(smokeEmitter, SmokeEmitterLifeTime);
    }
    private void Start()
    {
        target = Players.CurrentPlayer.transform;
        agent.avoidancePriority = Random.Range(0, 100);
    }

    private void MyHealth_OnDead(object sender, System.EventArgs e)
    {
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
        if (Time.time >= nextAttack)
        {
            nextAttack = Time.time + AttackInterval;
            myAnimator.SetTrigger(Attack1AnimationName);
        }
    }
    private void UpdateMovementAnimation()
    {
        myAnimator.SetFloat(MovementAnimationName, agent.velocity.magnitude / PursuitSpeed);
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
            Pursuit();
            if (Vector3.Distance(target.position, transform.position) <= AttackRange)
            { FaceTarget(); Attack(); }
            UpdateMovementAnimation();
        }
        else
        { agent.destination = transform.position; }
    }
}
