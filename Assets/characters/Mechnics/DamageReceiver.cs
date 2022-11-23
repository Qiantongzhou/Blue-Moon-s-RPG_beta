using UnityEngine;
public class DamageReceiver : MonoBehaviour
{
    [SerializeField]
    private Health myHealth;
    [SerializeField]
    private bool IsMonster;
    private void Awake()
    {
        if (IsMonster)
        {
            Monsters.AddDamageReceiver(gameObject);
        }
        else
        {
            Players.AddDamageReceiver(gameObject);
        }
    }
    public void ReceiveDamage(float damageAmount, Vector3 direction)
    {
        if (myHealth.IsAlive) { myHealth.HealthChange(-damageAmount, direction); }
    }
}
