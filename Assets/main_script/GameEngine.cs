using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] All_NPC;
    public GameObject[] NPC_POS;
    void Start()
    {
        unity_diceng.NPC_GEN = All_NPC;
        unity_diceng.NPC_POS= NPC_POS;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
