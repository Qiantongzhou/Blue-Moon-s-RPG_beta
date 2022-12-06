using UnityEngine;
public interface IDamageReceiver
{
    public void ReceiveDamage(float damageAmount, Vector3 direction);
}
