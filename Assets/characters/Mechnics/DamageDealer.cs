using UnityEngine;
public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private float DamageAmount;

    private HitAudio hitAudio;

    private void Awake()
    {
        hitAudio = GetComponent<HitAudio>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (EnemyMask == (EnemyMask | (1 << other.gameObject.layer)))
        {
            hitAudio.Hit();
            if (other.GetComponent<Health>() != null)
            {
                Vector3 direction = transform.position - other.gameObject.transform.position;
                Vector3 direction2D = new Vector3(direction.x, 0f, direction.z);
                DealDamage(other.GetComponent<Health>(), Vector3.Normalize(direction2D));
            }
        }
    }
    private void DealDamage(Health health, Vector3 direction)
    {
        health.TakeDamage(DamageAmount, direction);
    }
}
