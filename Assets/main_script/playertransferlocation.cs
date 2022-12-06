using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertransferlocation : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    public GameObject[]pointlocation;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

  public void changelocationto(int location)
    {
        player.transform.position = pointlocation[location].transform.position;
    }
}
