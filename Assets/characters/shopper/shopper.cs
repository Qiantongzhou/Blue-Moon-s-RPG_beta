using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shopper : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] enter;
    public AudioClip[] stay;
    public AudioClip[] buy;
    public AudioClip[] exit;
    public GameObject indicate;
    public GameEngine shop;
    GameObject temp;
    AudioSource source;
    float timer;
    bool playsound;
    private void Start()
    {
        playsound = true;
        source=gameObject.transform.AddComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            
            temp=Instantiate(indicate, GameObject.Find("Canvas").transform);
            temp.transform.SetAsLastSibling();
            if (enter != null)
            {
                source.clip = enter[Random.Range(0, 3)];
                source.Play();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {



            timer += Time.deltaTime;
        }
        if (timer > 10 && playsound)
        {
            if (stay != null)
            {
                source.clip = stay[Random.Range(0, 3)];
                source.Play();
                playsound = false;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player")&&timer>10)
        {
            if (exit != null)
            {
                source.clip = exit[Random.Range(0, 3)];
                source.Play();
            }
        }
        Destroy(temp);
        timer = 0;
    }

}
