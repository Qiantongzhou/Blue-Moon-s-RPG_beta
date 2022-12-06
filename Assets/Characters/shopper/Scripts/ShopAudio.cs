using UnityEngine;

public class ShopAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] EnteringAudioClips,
        StayingAudioClips,
        BuyingAudioClips,
        ExitingAudioClips;
    public float StayingAudioInterval = 10;

    private AudioSource audioSource;
    private float nextStayingAudioAt;
    private bool playExitAudio = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Players.CurrentPlayer)
        {
            if (EnteringAudioClips.Length > 0)
            {
                audioSource.PlayOneShot(AudioManager.GetRandomClip(EnteringAudioClips));
                nextStayingAudioAt = Time.time + StayingAudioInterval;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Players.CurrentPlayer)
        {
            if (Time.time > nextStayingAudioAt)
            {
                if (StayingAudioClips.Length > 0)
                {
                    audioSource.PlayOneShot(AudioManager.GetRandomClip(StayingAudioClips));
                    nextStayingAudioAt = Time.time + StayingAudioInterval;
                    playExitAudio = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            if (playExitAudio)
            {
                if (ExitingAudioClips.Length > 0)
                {
                    audioSource.PlayOneShot(AudioManager.GetRandomClip(ExitingAudioClips));
                }
            }
        }
    }

}
