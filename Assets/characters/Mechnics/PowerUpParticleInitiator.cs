using UnityEngine;

public class PowerUpParticleInitiator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem myParticleSystem;
    [SerializeField]
    private AudioClip[] audioClips;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Power()
    {
        myParticleSystem.Play();
        audioSource.PlayOneShot(AudioManager.GetRandomClip(audioClips));
    }
}
