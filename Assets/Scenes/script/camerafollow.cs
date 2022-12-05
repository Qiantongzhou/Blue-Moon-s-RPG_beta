using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class camerafollow : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;


    void Start()
    {
        mTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    void Update()
    {
      
        if (mTarget != null)
        {
            transform.position = new Vector3(mTarget.transform.position.x, mTarget.transform.position.y+20, mTarget.transform.position.z);

            // If close enough, just step over
            transform.rotation = Quaternion.Euler(90, mTarget.eulerAngles.y, 0);

        }

    }
}
