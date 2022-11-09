using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class meleattackauto : MonoBehaviour
{
    Animator anims;
    bool attacked;
    enemy enemy;
    damage_col dam;
    void Start()
    {
        enemy = transform.GetComponent<enemy>();
         
        
    }

    // Update is called once per frame
    void Update()
    {
        dam = enemy.target.GetComponent<damage_col>();
        anims = GetComponent<Animator>();
        if (Mathf.Abs(transform.position.x - enemy.target.transform.position.x) < enemy.stoprange && Mathf.Abs(transform.position.z - enemy.target.transform.position.z) < enemy.stoprange)
        {
            anims.SetBool("Attack 1", true);

            if (anims.GetCurrentAnimatorStateInfo(1).IsName("Attack 1"))
            {
                
                if (Mathf.FloorToInt(anims.GetCurrentAnimatorStateInfo(1).normalizedTime * 10 % 10) == 6)
                {

                    attacked = false;
                };

                if (Mathf.FloorToInt(anims.GetCurrentAnimatorStateInfo(1).normalizedTime * 10 % 10) == 5 && !attacked)
                {
                   
                    dam.takedamage(10);
                    attacked = true;
                }

            }

        }
        else
        {
            anims.SetBool("Attack 1", false);
        }
    }
}
