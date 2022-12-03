using UnityEngine;
public class DamageDealer : MonoBehaviour
{
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private float DamageAmount;

    private HitAudio hitAudio;
    private Collider myCollider;
    private void Awake()
    {
        hitAudio = GetComponent<HitAudio>();
        myCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (EnemyMask == (EnemyMask | (1 << other.gameObject.layer)))
        {
            Shield shield = other.GetComponent<Shield>();
            if (shield is not null)
            {
                // Other is a shield
                // Disable Self
                myCollider.enabled = false;
                // Call other's blocked
                ShieldController shieldController = shield.BelongTo.GetComponent<ShieldController>();
                shieldController.BlockHit();
                shield.PlayBlockHitAudio();
            }
            else
            {
                IDamageReceiver receiver = other.GetComponent<IDamageReceiver>();
                if (receiver is null) { return; }
                if (receiver is DamageReceiver) { hitAudio.Hit(); }
                else { myCollider.enabled = false; }
                Vector3 direction = transform.position - other.gameObject.transform.position;
                Vector3 direction2D = new Vector3(direction.x, 0f, direction.z);

                DealDamage(receiver, Vector3.Normalize(direction2D));
            }
        }
    }
    private void DealDamage(IDamageReceiver damageReceiver, Vector3 direction)
    {
        damageReceiver.ReceiveDamage(DamageAmount, direction);
    }
}
