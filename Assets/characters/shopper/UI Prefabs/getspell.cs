using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getspell : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] items;

    private void Start()
    {
        items = GameObject.Find("GameEngine").GetComponent<setitems>().randomespelllist;
        for (int i = 0; i < 20; i++)
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
