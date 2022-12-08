using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBallAudio : MonoBehaviour
{
    [SerializeField]
    private float AudioInterval = 0.5f;
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    private float nextPlayAt;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        nextPlayAt = Time.time;
    }
    private void Update()
    {
        if (Time.time >= nextPlayAt)
        {
            PlayAudio();
            nextPlayAt = Time.time + AudioInterval;
        }
    }
    private void PlayAudio()
    {
        audioSource.PlayOneShot(AudioManager.GetRandomClip(audioClips));
    }
}
