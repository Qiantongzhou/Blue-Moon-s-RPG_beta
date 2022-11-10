using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movelimit : MonoBehaviour
{
    // Start is called before the first frame update
    public float xmax;
    public float xmin;


    // Update is called once per frame
    void Update()
    {
        if(transform.position.z> xmax)
        {
            transform.position=new Vector3(transform.position.x,transform.position.y,xmax);
        }
        if (transform.position.z < xmin)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, xmin);
        }
    }
}
