using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermenu : MonoBehaviour
{
   
    void Start()
    {
        
    }

public void hover(int childnum)
    {
        transform.GetChild(childnum).gameObject.SetActive(true);
    }
    public void inactive(int childnum)
    {
        transform.GetChild(childnum).gameObject.SetActive(false);
    }

}
