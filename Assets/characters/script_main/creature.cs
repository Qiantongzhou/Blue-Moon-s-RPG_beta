using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creature : MonoBehaviour
{
    public float speed;
    Vector3 direction;
    Animator anims;
    float counttime;

    public float health;
    float currenthealth;
    bool die;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        if (DamageCalculator.multiPerEnemy != 0)
        {
            health = health * DamageCalculator.multiPerEnemy;
        }
        currenthealth = health;
        anims = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        die= false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (direction == Vector3.zero&&!die)
        {
            direction = new Vector3(Random.Range(100, -100), 2, Random.Range(100, -100));
        }
        if (anims.GetCurrentAnimatorStateInfo(0).IsName("walk") && !die)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            // Determine which direction to rotate towards

            Vector3 newdir =transform.position - direction ;
            // The step size is equal to speed times frame time.s
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, newdir, singleStep, 0.0f);
            
            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
            int time =Mathf.FloorToInt( counttime += Time.deltaTime);
            anims.SetInteger("walking", time);
        }else
   
        if (anims.GetCurrentAnimatorStateInfo(1).IsName("run") && !die)
        {
            transform.position = Vector3.MoveTowards(transform.position,target.transform.position, -5*speed * Time.deltaTime);
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
            int time = Mathf.FloorToInt(counttime += Time.deltaTime);
            anims.SetInteger("running", time);
        }
        else

        if (anims.GetCurrentAnimatorStateInfo(1).IsName("walk") && !die)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -1 * speed * Time.deltaTime);
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
            int time = Mathf.FloorToInt(counttime += Time.deltaTime);
            anims.SetInteger("running", time);
        }
        else

        if (anims.GetCurrentAnimatorStateInfo(2).IsName("run") && !die)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, -3 * speed * Time.deltaTime);
            // Determine which direction to rotate towards
            Vector3 targetDirection = direction - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
            int time = Mathf.FloorToInt(counttime += Time.deltaTime);
            anims.SetInteger("running", time);
        }
        else

        if (anims.GetCurrentAnimatorStateInfo(2).IsName("walk") && !die)
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, -1 * speed * Time.deltaTime);
            // Determine which direction to rotate towards
            Vector3 targetDirection = direction - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
            int time = Mathf.FloorToInt(counttime += Time.deltaTime);
            anims.SetInteger("running", time);
        }
        else
        {
            counttime = 0;
            direction = Vector3.zero;
            anims.SetInteger("running", 0);
            anims.SetInteger("walking", 0);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "ground")
        {
            anims.SetBool("alter", true);
        }
    }
    public void takedamage(int amount)
    {
        
        if (currenthealth > 0)
        {
            currenthealth -= amount;
            anims.SetBool("Hurt", true);
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
        anims.SetBool("Dead", true);
        Destroy(gameObject, 4);


    }
}
