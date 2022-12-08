using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nutralattack : MonoBehaviour
{
    Animator anims;
    bool attacked;
    nutral enemy;
    damage_col dam;

    void Start()
    {
        enemy = transform.GetComponent<nutral>();


    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.target != null)
        {

            dam = enemy.target.GetComponent<damage_col>();


            anims = GetComponent<Animator>();

            if (Mathf.Abs(transform.position.x - enemy.target.transform.position.x) < enemy.stopRange && Mathf.Abs(transform.position.z - enemy.target.transform.position.z) < enemy.stopRange)
            {
                anims.SetBool("attack", true);

                if (anims.GetCurrentAnimatorStateInfo(1).IsName("attack"))
                {

                    if (Mathf.FloorToInt(anims.GetCurrentAnimatorStateInfo(1).normalizedTime * 10 % 10) == 6)
                    {

                        attacked = false;
                    };

                    if (Mathf.FloorToInt(anims.GetCurrentAnimatorStateInfo(1).normalizedTime * 10 % 10) == 5 && !attacked)
                    {
                        if (dam != null)
                        {
                            dam.takedamage(Mathf.FloorToInt(enemy.attackDamage));
                        }
                       
                        attacked = true;
                    }

                }

            }
            else
            {
                anims.SetBool("attack", false);
            }
        }
    }
}
