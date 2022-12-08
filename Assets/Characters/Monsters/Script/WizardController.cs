using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class WizardController : MonoBehaviour
{
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private GameObject PainPrefab,
        SmokeEmitterPrefab;
    [SerializeField]
    private float AttackRange = 2, AttackInterval = 3,
        PursuitSpeed = 3,
        AngularSpeed = 1,
        SummonRange = 10,
        SmokeEmitterLifeTime = 2, DeathToSmokeInterval = 10, SmokeToCleatCorpseInterval = 1;
    [SerializeField]
    private int PainCount = 6;

    protected const string 
        animationParameter_Movement = "Movement",
        animationParameter_Attack = "Attack",
        animationParameter_Hurt = "Hurt", 
        animationParameter_Dead = "Dead";

    private float nextAttack = 0f;

    private Transform target;
    private NavMeshAgent agent;
    private Animator myAnimator;
    private Health myHealth;
    private List<GameObject> Pains;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        myHealth = GetComponent<Health>();
        myHealth.OnDead += MyHealth_OnDead;
        myHealth.OnHurt += MyHealth_OnHurt;
        Pains = new List<GameObject>();
        GameObject smokeEmitter = Instantiate(SmokeEmitterPrefab, transform.position, transform.rotation);
        Destroy(smokeEmitter, SmokeEmitterLifeTime);
    }
    private void Start()
    {
        target = Players.CurrentPlayer.transform;
        for (int i = 0; i < PainCount; i++)
        { SpawnPain(); }
    }

    private void MyHealth_OnDead(object sender, System.EventArgs e)
    {
        GameObject[] tempPains = new GameObject[PainCount];
        for (int i = 0; i < Pains.Count; i++)
        {
            GameObject pain = Pains[i];
            tempPains[i] = pain;
        }
        foreach (GameObject pain in tempPains)
        {
            Health painHealth = pain.GetComponent<Health>();
            painHealth.HealthChange(-painHealth.HealthPoint(), Vector3.zero);
        }
        myAnimator.SetFloat(animationParameter_Movement, 0);
        myAnimator.SetTrigger(animationParameter_Dead);
        StartCoroutine(Dead());
    }
    private void MyHealth_OnHurt(object sender, Vector3 e)
    {
        myAnimator.SetTrigger(animationParameter_Hurt);
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(DeathToSmokeInterval);
        GameObject smokeEmitter = Instantiate(SmokeEmitterPrefab, transform.position, transform.rotation);
        Destroy(smokeEmitter, SmokeEmitterLifeTime);
        yield return new WaitForSeconds(SmokeToCleatCorpseInterval);
        Destroy(gameObject);
    }
    private void SpawnPain()
    {
        Vector3 randomPosition = transform.position + Random.Range(-SummonRange, SummonRange) * transform.right + Random.Range(-SummonRange, SummonRange) * transform.forward;
        GameObject pain = Instantiate(PainPrefab, randomPosition, transform.rotation);
        pain.GetComponent<Health>().OnDead += Pain_OnDead;
        Pains.Add(pain);
    }
    private void Pain_OnDead(object sender, System.EventArgs e)
    {
        GameObject pain = ((GameObject)sender);
        Pains.Remove(pain);
        if (gameObject is not null && gameObject.GetComponent<Health>().IsAlive)
        {
            SpawnPain();
        }
        Debug.Log(Pains.Count);
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
            myAnimator.SetTrigger(animationParameter_Attack);
        }
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
            Pursuit();
            if (Vector3.Distance(target.position, transform.position) <= AttackRange)
            { FaceTarget(); Attack(); }
            UpdateMovementAnimation();
        }
        else
        { agent.destination = transform.position; }
    }
}
