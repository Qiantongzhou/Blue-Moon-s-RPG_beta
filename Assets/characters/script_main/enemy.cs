using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public GameObject target;
    public Canvas[] healthbar = new Canvas[3];
    Canvas bar;
    Animator anim;

    public float range;
    public float stopRange;
    public float speed;
    public float health;
    public float attackDamage;
    public float healthRegen;
    public float dir;

    float currenthealth;
    bool die;
    float timecaculate;
    void Awake()
    {
        
        GetComponent<TagEntity>().UpdateAttr();
        healthbar[0] = ((GameObject)Resources.Load("healthdisplayboss")).GetComponent<Canvas>();
        health = health * DamageCalculator.multiPerEnemy;
        attackDamage = attackDamage * DamageCalculator.multiPerEnemy;
        currenthealth = health;
        anim = GetComponent<Animator>();
        die = false;
       
        if (transform.tag == "Boss")
        {

            bar = Instantiate(healthbar[0], transform);
            bar.GetComponentInChildren<Slider>().value = currenthealth / health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("defensetarget");
        }
        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
        }
        if (target != null && !die)
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) < range && (Mathf.Abs(transform.position.x - target.transform.position.x) > stopRange || Mathf.Abs(transform.position.z - target.transform.position.z) > stopRange))
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                // Determine which direction to rotate towards
                Vector3 targetDirection = target.transform.position - transform.position;
                if (dir == -1)
                {
                    targetDirection = transform.position - target.transform.position;
                }

                // The step size is equal to speed times frame time.
                float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, target.transform.position, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
                anim.SetFloat("Movement", 1);
            }
            else
            {
                anim.SetFloat("Movement", 0);

            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player"|| collision.collider.tag=="playersattack")
        {
            target = GameObject.FindWithTag("Player");
        }
    }
    private void FixedUpdate()
    {
        if (bar != null)
        {
            bar.GetComponentInChildren<Slider>().value = currenthealth / health;
            TMP_Text[] x = bar.GetComponentsInChildren<TMP_Text>();
            x[0].text = currenthealth.ToString() + "/" + health.ToString();
            x[1].text = transform.name;
        }
        healthregenpersec();
    }
    private void healthregenpersec()
    {
        if (currenthealth < health)
        {
            timecaculate += Time.deltaTime;
            if (timecaculate > 1.0f)
            {
                currenthealth = currenthealth + healthRegen;
                timecaculate = 0.0f;
            }
        }
    }
    public void takedamage(int amount)
    {
        if (currenthealth > 0)
        {
            currenthealth -= amount;
            anim.SetBool("Hurt", true);
        }
        if (currenthealth <= 0)
        {
            currenthealth = 0;
            die = true;
            dead();
        }

    }
    public void dead()
    {
        anim.SetBool("Dead", true);
        Destroy(gameObject, 4);


    }

    public void InitializeAttr(float range, float stopRange, float speed, float health, float attackDamage, float healthRegen,float dir)
    {
        this.range = range;
        this.stopRange = stopRange;
        this.speed = speed;
        this.health = health;
        this.attackDamage = attackDamage;
        this.healthRegen = healthRegen;
        this.dir = dir;
    }
    private void OnDestroy()
    {
        // Destroy(GameObject.Find("healthdisplayboss(Clone)"));
    }
}
