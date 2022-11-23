using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    private Animator myAnimator;
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void DisableRootMotion()
    {
        myAnimator.applyRootMotion = false;
    }
    private void EnableRootMotion()
    {
        myAnimator.applyRootMotion = true;
    }
}
