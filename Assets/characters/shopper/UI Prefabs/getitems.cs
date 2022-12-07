using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getitems : MonoBehaviour
{
    public GameObject[] items;

    private void Start()
    {
        items = GameObject.Find("GameEngine").GetComponent<setitems>().randomeitems;
        for(int i=0; i< 20; i++)
        {
            if (items.Length > i)
            {
                if (items[i] != null)
                {
                    Instantiate(items[i], transform.GetChild(i).GetChild(0).transform);
                }
            }
        }
    }

}
