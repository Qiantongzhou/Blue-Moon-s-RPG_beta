using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameramove : MonoBehaviour
{
    public string nextscene;
    float time=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(transform.position.x + Time.deltaTime, transform.position.y , transform.position.z);
        time += Time.deltaTime;
        if (time > 15)
        {
            SceneManager.LoadScene(nextscene);
        }
    }
}
