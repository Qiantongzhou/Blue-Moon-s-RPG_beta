using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoney : MonoBehaviour
{
    [SerializeField]
    private float minAmount;
    [SerializeField]
    private float maxAmount;


    private void OnDestroy()
    {
        GameObject.FindWithTag("Player").GetComponent<player>().GiveMoney(Random.Range(minAmount,maxAmount));
    }
}
