using UnityEngine;

public class FootStep : MonoBehaviour
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
        audioSource.PlayOneShot(GetRandomClip(stepClips));
    }
    private void RunStep()
    {
        audioSource.PlayOneShot(GetRandomClip(runStepClips));
    }
    private AudioClip GetRandomClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length)];
    }
}
