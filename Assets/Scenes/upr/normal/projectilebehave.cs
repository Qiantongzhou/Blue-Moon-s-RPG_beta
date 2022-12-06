using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilebehave : MonoBehaviour
{
    public float speed;
    public float firerate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=transform.forward*speed*Time.deltaTime;
        firerate-= Time.deltaTime;
        if(firerate < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("col:"+collision.collider.name+" with: "+gameObject.name);
        speed = 0;
        Destroy(gameObject);
    }
}
