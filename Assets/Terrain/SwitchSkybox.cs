using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSkybox : MonoBehaviour
{
    public GameObject sheshou;
    public Material[] skyboxList;


    bool isForest;
    bool isVillage;
    bool isWinter;

    [SerializeField] LayerMask forestlayer;
    [SerializeField] LayerMask villagelayer;
    [SerializeField] LayerMask winterlayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isVillage)
        {

        }
        else if (isForest)
        {
            RenderSettings.skybox = skyboxList[1];
        }
    }

    void CheckIsForest()
    {
        isForest = Physics.Raycast(transform.position, Vector3.down, 30.0f, forestlayer);
    }

    void CheckIsVillage()
    {
        isForest = Physics.Raycast(transform.position, Vector3.down, 30.0f, forestlayer);
    }

    void CheckIsWinter()
    {
        isForest = Physics.Raycast(transform.position, Vector3.down, 30.0f, forestlayer);
    }
}
