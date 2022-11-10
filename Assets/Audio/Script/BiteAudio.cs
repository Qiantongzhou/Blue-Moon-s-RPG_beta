using UnityEngine;

public class BiteAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] biteClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Bite()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(biteClips));
    }
}
