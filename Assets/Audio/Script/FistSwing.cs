using UnityEngine;

public class FistSwing : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] fistSwingClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Swing()
    {
        audioSource.PlayOneShot(GetRandomClip(fistSwingClips));
    }
    private AudioClip GetRandomClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
