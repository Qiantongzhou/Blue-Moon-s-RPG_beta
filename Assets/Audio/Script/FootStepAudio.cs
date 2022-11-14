using UnityEngine;

public class FootStepAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] stepClips;
    [SerializeField]
    private AudioClip[] runStepClips;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Step()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(stepClips));
    }
    private void RunStep()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(runStepClips));
    }
}
