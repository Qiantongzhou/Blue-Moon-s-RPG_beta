using UnityEngine;

public class LightningBallLaucher : MonoBehaviour
{
    [SerializeField]
    private GameObject LighningBallPrefab,
        InstantiatePlace;
    [SerializeField]
    private ParticleSystem myParticleSystem;
    [SerializeField]
    private float LauchForce = 10;
    [SerializeField]
    private bool IsTargetingMonsters;
    private GameObject LightningBall;
    private void Awake()
    {
        if (IsTargetingMonsters)
        {
            Monsters.OnAddingMonsterDamageReceiver += Monsters_OnAddingMonsterDamageReceiver;
        }
        else
        {
            Players.OnAddingPlayerDamageReceiver += Player_OnPlayerChange;
        }
    }

    private void Player_OnPlayerChange(object sender, GameObject e)
    {
        myParticleSystem.trigger.AddCollider(e.GetComponent<Collider>());
    }

    private void Monsters_OnAddingMonsterDamageReceiver(object sender, GameObject e)
    {
        myParticleSystem.trigger.AddCollider(e.GetComponent<Collider>());
    }

    private void InstantiateLightningBall()
    {
        LightningBall = Instantiate(LighningBallPrefab, InstantiatePlace.transform.position, InstantiatePlace.transform.rotation);
    }
    private void LauchLightningBall()
    {
        Rigidbody rb = LightningBall.GetComponent<Rigidbody>();
        if (rb is not null)
        {
            rb.AddForce(InstantiatePlace.transform.forward * LauchForce, ForceMode.Impulse);
        }
        LightningBallController lightningBallController = LightningBall.GetComponent<LightningBallController>();
        if (lightningBallController is not null)
        {
            lightningBallController.Launch();
        }
    }

}
