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
            if (other.GetComponent<DamageReceiver>() != null)
            {
                Debug.Log("My name " + this.gameObject.name);
                Debug.Log("Other name " + other.name);
                hitAudio.Hit();
                Vector3 direction = transform.position - other.gameObject.transform.position;
                Vector3 direction2D = new Vector3(direction.x, 0f, direction.z);

                DealDamage(other.GetComponent<DamageReceiver>(), Vector3.Normalize(direction2D));
            }
        }
    }
    private void DealDamage(DamageReceiver damageReceiver, Vector3 direction)
    {
        damageReceiver.ReceiveDamage(DamageAmount, direction);
    }
}
