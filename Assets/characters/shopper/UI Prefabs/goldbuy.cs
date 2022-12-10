using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldbuy : MonoBehaviour
{
public void selectgold()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void buy()
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }
}
