using UnityEngine;

public class VoiceAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] shoutClips;
    [SerializeField]
    private AudioClip[] deadClips;
    [SerializeField]
    private AudioClip[] hurtClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Dead()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(deadClips));
    }
    private void Shout()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(shoutClips));
    }
    private void Hurt()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(hurtClips));
    }
}
