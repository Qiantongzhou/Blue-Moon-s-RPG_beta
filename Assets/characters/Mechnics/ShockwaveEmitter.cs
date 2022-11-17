using UnityEngine;

public class ShockwaveEmitter : MonoBehaviour
{
    [SerializeField]
    private float ExpendingSpeed,
        LifeTime,
        Force;
    [SerializeField]
    private bool IsLinear;
    [SerializeField]
    private LayerMask EnemyMask;

    private float DeathTime = 0f;
    private Material material;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        DeathTime = Time.time + LifeTime;
    }
    private void Update()
    {
        if (Time.time < DeathTime)
        {

            if (IsLinear)
            {
                transform.localScale += Vector3.one * ExpendingSpeed;
            }
            else
            {
                transform.localScale *= ExpendingSpeed;
            }
            if (material.GetFloat("_AlphaClip") < 1f)
            {
                material.SetFloat("_AlphaClip", 1 - ((DeathTime - Time.time) / LifeTime));
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (EnemyMask == (EnemyMask | (1 << other.gameObject.layer)))
        {
            if (other.GetComponent<Rigidbody>() is not null)
            {
                Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
                otherRigidbody.AddExplosionForce(Force, transform.position, transform.lossyScale.x, 0f, ForceMode.Impulse);
            }
        }
    }
}
