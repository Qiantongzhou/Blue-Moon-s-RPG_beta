using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

public class sheshouanime : MonoBehaviour
{
    public float movespeed;
    public float turnrate;

    //aim
    public Camera cam;
    public float maxlen;
    private Ray mouse;
    private Vector3 pos;
    private Vector3 direction;
    private quaternion rotation;

    //shooting
    public GameObject firepoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effect;
    private bool fired=false;
    public float attackspeed;

    bool running;
    bool walking = false;
    bool attacking = false;
    bool movelock = false;

    float horizontal = 0;
    float vertical = 0;
    [SerializeField]
     player aplayer;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        effect = vfx[0];
        

    }

    // Update is called once per frame
    void Update()
    {//aim
        RaycastHit hit;
        var mousePos = Input.mousePosition;
        mouse = cam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(mouse.origin, mouse.direction, out hit, maxlen))
        {
            rotatem(gameObject, hit.point);
        }
        else
        {
            var pos = mouse.GetPoint(maxlen);
            rotatem(gameObject, pos);
        }

       // MOVE
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackA 0"))
        {

            if (Mathf.FloorToInt(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 10%10) == 6)
            {
                
                fired = false; 
            };
            movelock = true;
            if (Mathf.FloorToInt(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 10%10) == 5&&!fired)
            {
                GameObject vfx;
                vfx = Instantiate(effect, firepoint.transform.position, Quaternion.identity);
                
                    vfx.transform.localRotation = GetRotation();
                fired = true;
            }
          
        }
        else
        {
            movelock = false;
        }
        if (Input.GetMouseButton(0) )
        {
            attacking = true;
            attackspeed = aplayer.attackspeed;
            animator.SetFloat("attackspeed", attackspeed);
           

        }
        else
        {
            attacking = false;
        }
        if (movelock)
        {
            horizontal = 0;
            vertical = 0;
        }
         
        if ((!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f)))
        {
           
                Vector3 direction = new Vector3(0.0f, 0.0f, vertical);
                direction = Vector3.ClampMagnitude(direction, 1.0f);
                transform.Translate(direction * movespeed * Time.deltaTime);
                transform.RotateAround(transform.position, Vector3.up, horizontal * turnrate * Time.deltaTime);
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
       
            animator.SetBool("walking", walking);
        
        animator.SetBool("attacking", attacking);
    }
    void rotatem(GameObject x, Vector3 y)
    {
        direction = y - x.transform.position;
        rotation = Quaternion.LookRotation(direction);
        
        x.transform.localRotation = Quaternion.Lerp(x.transform.rotation, rotation, 1);
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }

}
