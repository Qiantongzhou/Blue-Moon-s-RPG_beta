using UnityEngine;

public class meleattackauto : MonoBehaviour
{
    Animator anims;
    bool attacked;
    enemy enemy;
    damage_col dam;
    defensetarget def;
    void Start()
    {
        enemy = transform.GetComponent<enemy>();


    }

    // Update is called once per frame
    void Update()
    {

        dam = enemy.target.GetComponent<damage_col>();

        def = enemy.target.GetComponent<defensetarget>();
        
        anims = GetComponent<Animator>();
        if (Mathf.Abs(transform.position.x - enemy.target.transform.position.x) < enemy.stopRange && Mathf.Abs(transform.position.z - enemy.target.transform.position.z) < enemy.stopRange)
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
                    if (dam != null)
                    {
                        dam.takedamage(Mathf.FloorToInt(enemy.attackDamage));
                    }
                    if(def != null)
                    {
                        def.takedamage(Mathf.FloorToInt(enemy.attackDamage));
                    }
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
