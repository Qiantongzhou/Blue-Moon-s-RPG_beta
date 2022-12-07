using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setitems : MonoBehaviour
{
    public GameObject[] spells;
    public GameObject[] items;
    
    public GameObject[] randomespelllist;
    public GameObject[] randomeitems;
    public float shopperwaittime;


    private void Start()
    {
        randomeitems[0]= items[0];
        randomespelllist[0]= spells[0];
        StartCoroutine(randomitems());
    }
    IEnumerator randomitems()
    {
        yield return new WaitForSeconds(shopperwaittime);
        for(int i = 0; i < items.Length; i++)
        {
            if (Random.Range(0, 100) > 50)
            {
                randomeitems[i] = items[i];
               
            }
            else
            {
                randomeitems[i] = null;
            }
        }
        for (int i = 0; i < spells.Length; i++)
        {
            if (Random.Range(0, 100) > 50)
            {
                randomespelllist[i] = spells[i];

            }
            else
            {
                randomespelllist[i] = null;
            }
        }
        StartCoroutine(randomitems());
    }
}
