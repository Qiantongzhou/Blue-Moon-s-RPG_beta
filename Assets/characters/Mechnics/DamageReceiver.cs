using UnityEngine;
public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    private Health myHealth;
    public void ReceiveDamage(float damageAmount, Vector3 direction)
    {
        if (myHealth.IsAlive) { myHealth.HealthChange(-damageAmount, direction); }
    }
}
