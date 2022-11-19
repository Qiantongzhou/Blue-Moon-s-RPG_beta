using UnityEngine;
public class FanAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] fanClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Fan()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(fanClips));
    }
}
