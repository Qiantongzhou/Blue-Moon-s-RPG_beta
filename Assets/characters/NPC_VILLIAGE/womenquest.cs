using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class womenquest : MonoBehaviour
{
    public GameObject help;
    public GameObject language1;
    public GameObject language2;
    GameObject a;
    GameObject b;
    GameObject pre;
    bool started=false;
    private void Start()
    {
        pre=Instantiate(help,GameObject.Find("Canvas").transform);
        pre.gameObject.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pre.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)&&!started)
            {
                started = true;
                startconversation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pre.gameObject.SetActive(false);
    }

    public void startconversation()
    {
        a = Instantiate(language1, GameObject.Find("Canvas").transform);
        StartCoroutine(wait1());
    }

    IEnumerator wait1()
    {
        yield return new WaitForSeconds(5);
        a.SetActive(false);
        StartCoroutine(wait2());
    }
    IEnumerator wait2()
    {
        b=Instantiate(language2, GameObject.Find("Canvas").transform);
        yield return new WaitForSeconds(5f);
        b.SetActive(false);
        GameObject.Find("GameEngine").GetComponent<GameEngine>().startwave = true;
    }
}
