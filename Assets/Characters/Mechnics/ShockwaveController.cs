using UnityEngine;

public class ShockwaveController : MonoBehaviour
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
        Debug.Log("Awake");
        material = GetComponent<Renderer>().material;
        DeathTime = Time.time + LifeTime;
    }
    private void Update()
    {
        Debug.Log("Update");
        if (Time.time < DeathTime)
        {
            if (IsLinear)
            { transform.localScale += Vector3.one * ExpendingSpeed * Time.deltaTime; }
            else
            {
                Debug.Log("Not Linear");
                transform.localScale *= 1 + (ExpendingSpeed * Time.deltaTime); }
            if (material.GetFloat("_AlphaClip") < 1f)
            { material.SetFloat("_AlphaClip", 1 - ((DeathTime - Time.time) / LifeTime)); }
        }
        else
        { Destroy(gameObject); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (EnemyMask == (EnemyMask | (1 << other.gameObject.layer)))
        {
            if (other.GetComponent<Rigidbody>() != null)
            {
                Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
                otherRigidbody.AddExplosionForce(Force, transform.position, transform.lossyScale.x, 0f, ForceMode.Impulse);
            }
        }
    }
}
