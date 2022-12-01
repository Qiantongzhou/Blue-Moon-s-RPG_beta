using UnityEngine;

public class PowerUpAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] castClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Power()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(castClips));
    }
}
