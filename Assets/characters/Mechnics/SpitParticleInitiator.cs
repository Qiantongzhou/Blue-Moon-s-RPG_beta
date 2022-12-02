using UnityEngine;

public class SpitParticleInitiator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem myParticleSystem;
    [SerializeField]
    private bool IsTargetingMonsters;
    private void Awake()
    {
        if (IsTargetingMonsters)
        {
            Monsters.OnAddingMonsterDamageReceiver += Monsters_OnAddingMonsterDamageReceiver;
        }
        else
        {
            Players.OnAddingPlayerDamageReceiver += Player_OnAddingPlayerDamageReceiver;
        }
    }

    private void Player_OnAddingPlayerDamageReceiver(object sender, GameObject e)
    {
        myParticleSystem.trigger.AddCollider(e.GetComponent<Collider>());
    }

    private void Monsters_OnAddingMonsterDamageReceiver(object sender, GameObject e)
    {
        myParticleSystem.trigger.AddCollider(e.GetComponent<Collider>());
    }

    private void Spit()
    {
        myParticleSystem.Play();
    }


}
