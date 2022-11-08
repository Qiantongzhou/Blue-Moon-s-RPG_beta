using UnityEngine;

public class Voice : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] shoutClips;
    [SerializeField]
    private AudioClip[] deadClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Dead()
    {
        audioSource.PlayOneShot(GetRandomClip(deadClips));
    }
    private void Shout()
    {
        audioSource.PlayOneShot(GetRandomClip(shoutClips));
    }
    private AudioClip GetRandomClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
