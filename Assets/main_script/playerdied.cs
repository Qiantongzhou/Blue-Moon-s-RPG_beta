using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerdied : MonoBehaviour
{
    public GameObject diescene;
    public GameObject quit;
    GameObject d;
    GameObject q;
    private void Start()
    {
        d=Instantiate(diescene,GameObject.Find("Canvas").transform);
        d.gameObject.SetActive(false);
        q = Instantiate(quit, GameObject.Find("Canvas").transform);
        q.GetComponent<Button>().onClick.AddListener(()=>GameObject.Find("EventSystem").GetComponent<LOADSCENE>().changesenceto("loadscreen_march"));

        q.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (gamestatistics.playerdied)
        {
            d.SetActive(true);
            StartCoroutine(quitgame());
        }
    }
    IEnumerator quitgame()
    {
        yield return new WaitForSeconds(0.1f);
        q.gameObject.SetActive(true );
    }
}
