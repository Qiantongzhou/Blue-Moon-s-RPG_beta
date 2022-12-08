using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10.0f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PaladinController>().HitBySnowBall();
        }
    }


}
