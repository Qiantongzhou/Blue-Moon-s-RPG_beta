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

    [SerializeField] LayerMask villagelayer;
    [SerializeField] LayerMask forestlayer;
    [SerializeField] LayerMask winterlayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsVillage();
        CheckIsForest();
        CheckIsWinter();

        if (isVillage)
        {
            RenderSettings.skybox = skyboxList[0];
            DynamicGI.UpdateEnvironment();
        }
        else if (isForest)
        {
            RenderSettings.skybox = skyboxList[1];
            DynamicGI.UpdateEnvironment();
        }
        else if (isWinter)
        {
            RenderSettings.skybox = skyboxList[2];
            DynamicGI.UpdateEnvironment();
        }
    }


    void CheckIsVillage()
    {
        isVillage = Physics.Raycast(transform.position, Vector3.down, 30.0f, villagelayer);
    }
    void CheckIsForest()
    {
        isForest = Physics.Raycast(transform.position, Vector3.down, 30.0f, forestlayer);
    }

    void CheckIsWinter()
    {
        isWinter = Physics.Raycast(transform.position, Vector3.down, 30.0f, winterlayer);
    }
}
