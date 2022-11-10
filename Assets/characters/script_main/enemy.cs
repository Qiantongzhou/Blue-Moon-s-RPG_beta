using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemy : MonoBehaviour
{
    public GameObject target;
    public float range;
    public float stoprange;
    public float speed;
     Animator anim;
    public float health;
    bool die;
    void Start()
    {
        anim = GetComponent<Animator>();
        die = false;
        target = GameObject.FindWithTag("Player");

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
    public void takedamage(int amount)
    {
        health-=amount;
        anim.SetBool("Hurt", true);
        if(health <= 0)
        {
            die = true;
            dead();
        }

    }
    public void dead()
    {
        anim.SetBool("Dead", true);
    }
}
