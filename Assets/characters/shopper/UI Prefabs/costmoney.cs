using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class costmoney : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float minAmount;
    [SerializeField]
    private float maxAmount;
    bool sell=false;
    private void OnTransformChildrenChanged()
    {
        if (sell)
        {
            GameObject.FindWithTag("Player").GetComponent<player>().GiveMoney(-Random.Range(minAmount, maxAmount));
             sell = false;
        }
        else
        {
            sell = true;
        }
    }
}
