
using System.Collections.Generic;

using UnityEngine;


public class character : MonoBehaviour
{
    public Camera cam;
    float horizontal = 0;
    float vertical = 0;

    //shoot
    public GameObject firepoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effect;
    private bool fired = false;

    //animation
    bool running;
    bool walking = false;
    bool attacking = false;
    bool movelock = false;
    Animator animator;


    //player class
    [SerializeField]
    public player aplayer;

  

    void Start()
    {
        animator = GetComponent<Animator>();
        effect = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
       
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackA 0"))
        {

            if (Mathf.FloorToInt(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 10 % 10) == 6)
            {

                fired = false;
            };
            movelock = true;
            if (Mathf.FloorToInt(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 10 % 10) == 5 && !fired)
            {
                GameObject vfx;
                vfx = Instantiate(effect, firepoint.transform.position, Quaternion.identity);
                vfx.tag = "playersattack";
                vfx.transform.localRotation = gameObject.transform.rotation;
                fired = true;
            }

        }
        else
        {
            movelock = false;
        }
        if (Input.GetMouseButton(0))
        {
            attacking = true;
         
            animator.SetFloat("attackspeed", aplayer.attackspeed);


        }
        else
        {
            attacking = false;
        }
        // MOVE
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (movelock)
        {
            horizontal = 0;
            vertical = 0;
        }
        if ((!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f)))
        {

            Vector3 direction = new Vector3(0.0f, 0.0f, vertical);
            direction = Vector3.ClampMagnitude(direction, 1.0f);
            int multipler = 2;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                running = true;
                int temp = aplayer.movespeed;
                if (temp > 10)
                {
                   
                    multipler = 1;
                }
                else
                {
                   
                    multipler = 2;
                }
            }
            else
            {
                running = false;
            }
            if (aplayer.movespeed > 3)
            {
                running = true;
            }
          
            transform.Translate(direction * aplayer.movespeed * multipler* Time.deltaTime);
            transform.RotateAround(gameObject.transform.position, Vector3.up, horizontal * aplayer.turnrate * Time.deltaTime);
            walking = true;

        }
        else
        {
            walking = false;
        }
        if (vertical < 0.0f)
        {
            animator.SetFloat("animedirection", -1.0f);
        }
        else
        {
            animator.SetFloat("animedirection", 1.0f);
        }

        updateanimator();

    }

    private void updateanimator()
    {
        animator.SetBool("running", running);

        animator.SetBool("walking", walking);

        animator.SetBool("attacking", attacking);
    }
    public void takedamage(int amount)
    {
        aplayer.takedamage(amount);
        animator.SetBool("hurt", true);
        if (aplayer.getcurrenthealth() <= 0)
        {
            die();
        }
    }
    public void die()
    {
        animator.SetBool("dead", true);
    }
}
