using System.Collections;
using System.Collections.Generic;
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
        if (collision.collider.tag == "playersattack")
        {
            takedamageboss(npc.aplayer.attackdamage);
        }
        
    }
    public void takedamageboss(int dam)
    {
        enemy x=GetComponent<enemy>();
        x.takedamage(dam);
       
    }
    public void takedamage(int dam)
    {
        
        npc.takedamage(dam);
        Updatetext(dam);
    }
    public void Updatetext(int dam)
    {
        textshowprefed.GetComponent<TMP_Text>().text=dam.ToString();
        showfloatingtext();
    }
    public void showfloatingtext()
    {
        
        GameObject text=Instantiate(textshowprefed, transform.position + offset, Quaternion.identity,transform);
      text.transform.transform.localScale =new Vector3(0.1f,0.1f,0.1f);
        Destroy(text, 1);
    }

}
