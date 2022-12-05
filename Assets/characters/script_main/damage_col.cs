using UnityEngine;
using TMPro;

public class damage_col : MonoBehaviour
{
    public GameObject textshowprefed;
    public Vector3 offset;
    character npc;
    private void Start()
    {
        npc = GameObject.FindWithTag("Player").GetComponent<character>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "playersattack" && transform.tag == "Boss")
        {
            takedamageboss(npc.aplayer.attr.attackdamage);
        }
        if (collision.collider.tag == "playersattack" && transform.tag == "creep")
        {
            takedamagecreep(npc.aplayer.attr.attackdamage);
        }
        if (collision.collider.tag == "playersattack" && transform.tag == "creature")
        {
            takedamagecreature(npc.aplayer.attr.attackdamage);
        }
    }
    public void takedamageboss(int dam)
    {
        enemy x = GetComponent<enemy>();
        x.takedamage(dam);

    }
    public void takedamagecreep(int dam)
    {
        enemy x = GetComponent<enemy>();
        x.takedamage(dam);
        Updatetext(dam);
    }
    public void takedamagecreature(int dam)
    {
        creature x = GetComponent<creature>();
        x.takedamage(dam);
        Updatetext(dam);
    }
    public void takedamage(int dam)
    {

        npc.takedamage(dam);
        Updatetext(dam);
    }
    public void Updatetext(int dam)
    {
        textshowprefed.GetComponent<TMP_Text>().text = dam.ToString();
        showfloatingtext();
    }
    public void showfloatingtext()
    {

        GameObject text = Instantiate(textshowprefed, transform.position + offset, Quaternion.identity, transform);

        print(text.transform.ToString());
        Destroy(text, 1);
    }

}
