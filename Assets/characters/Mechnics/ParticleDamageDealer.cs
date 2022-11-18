using UnityEngine;
using System.Collections.Generic;

public class ParticleDamageDealer : MonoBehaviour
{
    [SerializeField]
    private LayerMask EnemyMask;
    [SerializeField]
    private float DamageAmout;

    private ParticleSystem myParticleSystem;

    private void Start()
    {
        // At all time, there should be only 1 collider set inside particle system's trigger.
        // Which is the current player object.
        myParticleSystem = GetComponent<ParticleSystem>();
        if (myParticleSystem.trigger.colliderCount == 1)
        {
            myParticleSystem.trigger.SetCollider(0, Player.GetCurrentPlayer().GetComponent<Collider>());
        }
        else
        {
            myParticleSystem.trigger.AddCollider(Player.GetCurrentPlayer().GetComponent<Collider>());
        }

    }
    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enteringParticles = new List<ParticleSystem.Particle>();
        int numOfParticleEntering = myParticleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteringParticles, out ParticleSystem.ColliderData colliderData);
        // If particle system's trigger is not set with exactly 1 collider.
        if (myParticleSystem.trigger.colliderCount != 1)
        {
            Debug.Log("ParticleSystem has " + myParticleSystem.trigger.colliderCount + " collider");
            return;
        }

        Collider opponentCollider = (Collider)myParticleSystem.trigger.GetCollider(0);
        GameObject opponent = opponentCollider.gameObject;
        DamageReceiver damageReceiver = opponent.GetComponent<DamageReceiver>();

        for (int i = 0; i < numOfParticleEntering; i++)
        {
            ParticleSystem.Particle particle = enteringParticles[i];
            // If particle enter exactly 1 collider marked by particle system
            if (colliderData.GetColliderCount(i) != 1)
            {
                continue;
            }
            if (colliderData.GetCollider(i, 0) == opponentCollider)
            {
                Vector3 direction = transform.position - opponent.transform.position;
                Vector3 direction2D = new Vector3(direction.x, 0f, direction.z);
                damageReceiver.ReceiveDamage(DamageAmout, direction2D);
            }
        }
    }
}