using UnityEngine;

public class SwingAudio : MonoBehaviour
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
        audioSource.PlayOneShot(AudioManager.GetRandomClip(fistSwingClips));
    }
}
