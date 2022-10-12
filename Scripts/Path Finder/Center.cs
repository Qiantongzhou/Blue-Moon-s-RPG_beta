using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (IsPlayer(other)) return;
        if (IsEnemy(other)) return;
    }
    private bool IsEnemy(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<SimpleEnemy>().StepIn();//need to change script

            return true;
        }
        return false;
    }
}
