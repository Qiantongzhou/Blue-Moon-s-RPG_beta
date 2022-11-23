using UnityEngine;

public class CastAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] castClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Bite()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(castClips));
    }
}
