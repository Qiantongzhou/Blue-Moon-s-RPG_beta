using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class damage_col : MonoBehaviour
{
    public GameObject textshowprefed;
    public GameObject textcritprefab;
    public AudioClip  crit;
    AudioSource crited;
    
    public Vector3 offset;
    character npc;
    private void Start()
    {
        npc = GameObject.FindWithTag("Player").GetComponent<character>();
        if (crit != null)
        {
            crited = gameObject.AddComponent<AudioSource>();
            crited.clip = crit;
            crited.playOnAwake = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "playersattack" && transform.tag == "Boss")
        {
            takedamageboss(collision.collider.GetComponent<ProjectileMover>().damage, collision.collider.GetComponent<ProjectileMover>().iscritic);
        }
        if (collision.collider.tag == "playersattack" && transform.tag == "creep")
        {
            takedamagecreep(collision.collider.GetComponent<ProjectileMover>().damage, collision.collider.GetComponent<ProjectileMover>().iscritic);
        }
        if (collision.collider.tag == "playersattack" && transform.tag == "creature")
        {
            takedamagecreature(collision.collider.GetComponent<ProjectileMover>().damage, collision.collider.GetComponent<ProjectileMover>().iscritic);
        }
        if (collision.collider.tag == "playersattack" && transform.tag == "nutral")
        {
            takedamagenutral(collision.collider.GetComponent<ProjectileMover>().damage, collision.collider.GetComponent<ProjectileMover>().iscritic);
        }
    }
    public void takedamageboss(int dam, bool iscrit)
    {
        enemy x = GetComponent<enemy>();
        x.takedamage(dam);

    }
    public void takedamagecreep(int dam,bool iscrit)
    {
        enemy x = GetComponent<enemy>();
        x.takedamage(dam);
        Updatetext(dam,iscrit);
    }
    public void takedamagecreature(int dam, bool iscrit)
    {
        creature x = GetComponent<creature>();
        x.takedamage(dam);
        Updatetext(dam, iscrit);
    }
    public void takedamagenutral(int dam, bool iscrit)
    {
        nutral x = GetComponent<nutral>();
        x.takedamage(dam);
        Updatetext(dam, iscrit);
    }
    public void takedamage(int dam)
    {

        npc.takedamage(dam);
        //Updatetext(dam, false); ;
    }
    public void Updatetext(int dam,bool iscrit)
    {
        textshowprefed.GetComponent<TMP_Text>().text = dam.ToString();
        if (textcritprefab != null)
        {
            textcritprefab.GetComponent<TMP_Text>().text = dam.ToString();
        }
        showfloatingtext(iscrit);
    }
    public void showfloatingtext(bool iscrit)
    {
        
        if (iscrit&&textcritprefab!=null)
        {
            GameObject text = Instantiate(textcritprefab, transform.position + offset, Quaternion.identity, transform);
            crited.Play();
            print(text.transform.ToString());
            Destroy(text, 1);
        }
        else
        {
            GameObject text = Instantiate(textshowprefed, transform.position + offset, Quaternion.identity, transform);

            print(text.transform.ToString());
            Destroy(text, 1);
        }
    }

}
