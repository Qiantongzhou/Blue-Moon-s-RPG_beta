using UnityEngine;
public class DamageReceiver : MonoBehaviour, IDamageReceiver
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
        Debug.DrawRay(transform.position, direction, Color.red, 5);
        if (myHealth.IsAlive) { myHealth.HealthChange(-damageAmount, direction); }
    }
}
