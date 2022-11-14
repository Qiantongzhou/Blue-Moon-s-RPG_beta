using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class camerafollow : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;

    float kFollowSpeed = 3.5f;
    float stepOverThreshold = 0.05f;
    float horizontal=0;
    void Start()
    {
       
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
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
