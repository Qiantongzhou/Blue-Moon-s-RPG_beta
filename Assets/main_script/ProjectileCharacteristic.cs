using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCharacteristic : MonoBehaviour
{
    [SerializeField]
    private int projectileProperty;
    [SerializeField]
    private LayerMask EnemyMask;

    public GameObject Origin;
    public ProjectileCharacteristic() { }

    private void OnTriggerEnter(Collider other)
    {
        //  
        //
    }
}
