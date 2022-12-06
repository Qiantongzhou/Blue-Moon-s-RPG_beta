using UnityEngine;
public class Shield : MonoBehaviour
{
    public GameObject BelongTo;
    [SerializeField]
    private AudioClip[] audioClips;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBlockHitAudio()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(audioClips));
    }

}
