using UnityEngine;

public class SpitParticleInitiator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem myParticleSystem;
    private void Spit()
    {
        myParticleSystem.Play();
    }

}
