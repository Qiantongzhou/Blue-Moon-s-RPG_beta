using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class aim : MonoBehaviour
{
    //aim
    public Camera cam;
    public float maxlen;
    private Ray mouse;
    private Vector3 pos;
    private Vector3 direction;
    private quaternion rotation;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            mouse = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(mouse.origin, mouse.direction, out hit, maxlen, 0))
            {
                rotatem(gameObject, hit.point);
            }
            else
            {
                var pos = mouse.GetPoint(maxlen);
                rotatem(gameObject, pos);
            }

        }
    }
    void rotatem(GameObject x, Vector3 y)
    {
        direction = y - x.transform.position;
        rotation = Quaternion.LookRotation(direction);

        x.transform.localRotation = Quaternion.Lerp(x.transform.rotation, rotation, 1);
    }

}
