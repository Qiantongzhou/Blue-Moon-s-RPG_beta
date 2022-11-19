
using UnityEngine;
public class HitAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] hitClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Hit()
    {
        Debug.Log("Hit " + this.gameObject.name);
        audioSource.PlayOneShot(AudioManager.GetRandomClip(hitClips));
    }
}
