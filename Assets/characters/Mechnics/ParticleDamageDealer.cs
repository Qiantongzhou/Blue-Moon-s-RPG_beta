using UnityEngine;
using System.Collections.Generic;

public class ParticleDamageDealer : MonoBehaviour
{
    [SerializeField]
    private float DamageAmout;

    private ParticleSystem myParticleSystem;

    private void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }
    
    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enteringParticles = new List<ParticleSystem.Particle>();
        int numOfParticleEntering = myParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteringParticles, out ParticleSystem.ColliderData colliderData);

        for (int i = 0; i < numOfParticleEntering; i++)
        {
            ParticleSystem.Particle particle = enteringParticles[i];
            Debug.Log(colliderData.GetColliderCount(i));
            for (int j = 0; j < colliderData.GetColliderCount(i); j++)
            {
                Debug.Log(colliderData.GetCollider(i, j).name);
                GameObject opponent = colliderData.GetCollider(i, j).gameObject;
                DamageReceiver damageReceiver = opponent.GetComponent<DamageReceiver>();
                Vector3 direction = transform.position - opponent.transform.position;
                Vector3 direction2D = new Vector3(direction.x, 0f, direction.z);
                damageReceiver.ReceiveDamage(DamageAmout, direction2D);
            }
        }
    }
}