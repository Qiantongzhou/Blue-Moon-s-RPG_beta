using UnityEngine;
public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private LayerMask EnemyMask;

    private Collider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (EnemyMask == (EnemyMask | (1 << other.gameObject.layer)))
        {
            DealDamage();
        }
    }
    private void DealDamage()
    {
        Debug.Log("Hit");
    }
}
