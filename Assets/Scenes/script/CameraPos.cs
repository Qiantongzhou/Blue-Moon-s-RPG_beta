using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public Transform lookAtTarget;
    // Start is called before the first frame update
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Vector3 LookAtOffset;
    public float radius;

    public float verticalAngle=15f;
    public float horizontalAngle=0f;


    private void Awake()
    {
        //lookAtTarget = GetComponentInParent<Transform>();
        //offset = transform.localPosition;
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        verticalAngle -= Input.GetAxisRaw("Mouse Y");
        verticalAngle = verticalDeadZone(verticalAngle);
        horizontalAngle += Input.GetAxisRaw("Mouse X");

        //camera position
        transform.position = new Vector3(radius * (float)Math.Sin((Math.PI / 180) * verticalAngle) * (float)Math.Sin((Math.PI / 180) *horizontalAngle)+ offset.x,
            radius * (float)Math.Cos((Math.PI / 180) * verticalAngle)+offset.y,
            radius * (float)Math.Sin((Math.PI / 180) * verticalAngle) * (float)Math.Cos((Math.PI / 180) * horizontalAngle)+offset.z);

        //camera look at
        transform.LookAt(lookAtTarget.position+LookAtOffset);


    }

    float verticalDeadZone(float num)
    {
        return Math.Max(Math.Min(num, -10), -95) ;
    }

}
