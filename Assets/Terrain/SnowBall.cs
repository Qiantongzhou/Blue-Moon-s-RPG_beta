using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public PaladinController paladin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("SnowBALL");
            paladin.transform.rotation = Quaternion.Euler(new Vector3(0, paladin.transform.rotation.eulerAngles.y, 0));
            Vector3 forceDir = new Vector3(10.0f, 3.0f, 20.0f);
            //paladin.rb.velocity = Vector3.zero;
            paladin.rb.AddRelativeForce(forceDir, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
}
