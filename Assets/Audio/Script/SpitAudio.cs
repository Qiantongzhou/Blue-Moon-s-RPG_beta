using UnityEngine;
public class SpitAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] spitClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Spit()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(spitClips));
    }
}
