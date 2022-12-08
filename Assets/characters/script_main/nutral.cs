using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nutral : MonoBehaviour
{
    public GameObject target;
 
    Animator anim;

    public float range;
    public float stopRange;
    public float speed;
    public float health;
    public float attackDamage;
    public float healthRegen;
    public float dir;
    bool die;
    float currenthealth;
    void Start()
    {
        health = health * (DamageCalculator.multiPerEnemy+1);
        attackDamage = attackDamage * (DamageCalculator.multiPerEnemy+1);
        currenthealth = health;
        anim = GetComponent<Animator>();
        die = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                anim.SetBool("running", true);
            }
            else
            {
                anim.SetBool("running", false);

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "playersattack")
        {
            target = GameObject.FindWithTag("Player");
        }
    }
    public void takedamage(int amount)
    {
        if (currenthealth > 0)
        {
            currenthealth -= amount;
            
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
        anim.SetBool("die", true);
        Destroy(gameObject, 4);


    }
}
