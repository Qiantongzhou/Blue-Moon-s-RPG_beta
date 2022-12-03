using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PhysicFootStepAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] stepClips;
    [SerializeField]
    private AudioClip[] runStepClips;
    [SerializeField]
    private float DistanceToGround;
    [SerializeField]
    private float DistanceToLeaveGround;
    private AudioSource audioSource;
    private bool isFootOnAir;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isFootOnAir = false;
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.forward, Color.red);
        if (isFootOnAir)
        {
            if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit raycastHit, DistanceToGround))
            {
                Step();
                isFootOnAir = false;
            }
        }
        else
        {
            if (!Physics.Raycast(transform.position, -transform.forward, DistanceToLeaveGround))
            {
                isFootOnAir = true;
            }
        }
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
