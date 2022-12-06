using UnityEngine;
public class BulletTimeTrigger : MonoBehaviour, IDamageReceiver
{
    [SerializeField]
    private float LifeTime = 1f;

    private float DeathTime;
    public BulletTimeController bulletTimeController;
    public void ReceiveDamage(float damageAmount, Vector3 direction)
    {
        bulletTimeController.StartBulletTime();
        Destroy(gameObject);
    }
    private void Start()
    {
        DeathTime = Time.time + LifeTime;
    }
    private void Update()
    {
        if (DeathTime < Time.time)
        {
            Destroy(gameObject);
        }
    }
}

