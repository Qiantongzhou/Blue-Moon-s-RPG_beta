using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class shieldeffect : MonoBehaviour
{

    public GameObject shieldef;
    private VisualEffect shieldeffects;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        var ripples=Instantiate(shieldef,transform)as GameObject;
        shieldeffects=ripples.GetComponent<VisualEffect>();
        shieldeffects.SetVector3("centre", collision.contacts[0].point);
       // Destroy(ripples, 2);
    }
}
