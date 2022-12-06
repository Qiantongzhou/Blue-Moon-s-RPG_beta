using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertransferlocation : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public GameObject[]pointlocation;
    public GameObject[] scene;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        scene[0].SetActive(true);
        scene[2].SetActive(false);
    }

  public void changelocationto(int location)
    {
        if (location == 0)
        {
            scene[0].SetActive(true);
            scene[2].SetActive(false);
        }

        if (location == 2)
        {
            scene[0].SetActive(false);
            scene[2].SetActive(false);
        }
        if(location == 1)
        {
            scene[0].SetActive(false);
            scene[2].SetActive(true);
        }
        player.transform.position = pointlocation[location].transform.position;
    }
}
