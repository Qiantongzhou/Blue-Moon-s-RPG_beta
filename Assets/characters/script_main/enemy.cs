using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class enemy : MonoBehaviour
{
    public GameObject target;
    public Canvas[] healthbar;
    Canvas bar;
    public float range;
    public float stoprange;
    public float speed;
     Animator anim;
    public float health;
    float currenthealth;
    public float attackdamage;
    public float healthregen;
    bool die;
    float timecaculate;
    void Start()
    {
        
        health = health * DamageCalculator.multiPerEnemy;
        attackdamage = attackdamage * DamageCalculator.multiPerEnemy;
        currenthealth = health;
        anim = GetComponent<Animator>();
        die = false;
        target = GameObject.FindWithTag("Player");
        if (transform.tag == "Boss")
        {

            bar = Instantiate(healthbar[0],transform);
            bar.GetComponentInChildren<Slider>().value = currenthealth / health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !die)
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) < range && (Mathf.Abs(transform.position.x - target.transform.position.x) > stoprange || Mathf.Abs(transform.position.z - target.transform.position.z) > stoprange))
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                // Determine which direction to rotate towards
                Vector3 targetDirection = target.transform.position - transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
                anim.SetFloat("Movement",1);
            }
            else
            {
                anim.SetFloat("Movement", 0);

            }
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
                currenthealth = currenthealth + healthregen;
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
        if(currenthealth <= 0)
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
    private void OnDestroy()
    {
       // Destroy(GameObject.Find("healthdisplayboss(Clone)"));
    }
}
