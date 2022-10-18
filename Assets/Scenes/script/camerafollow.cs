using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;

    float kFollowSpeed = 3.5f;
    float stepOverThreshold = 0.05f;

    void Update()
    {
        if (mTarget != null)
        {
            Vector3 targetPosition = new Vector3(mTarget.transform.position.x, transform.position.y, mTarget.transform.position.z-2);
            Vector3 direction = targetPosition - transform.position;

            if (direction.magnitude > stepOverThreshold)
            {
                // If too far, translate at kFollowSpeed
                transform.Translate(direction.normalized * kFollowSpeed * Time.deltaTime);
            }
            else
            {
                // If close enough, just step over
                transform.position = targetPosition;
            }
        }
    }
}
