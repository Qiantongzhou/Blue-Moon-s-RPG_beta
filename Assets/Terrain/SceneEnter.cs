using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnter : MonoBehaviour
{
    public string lastExitName;

    void Start()
    {
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            PlayerTransfer.instance.transform.position = transform.position;
            PlayerTransfer.instance.transform.eulerAngles = transform.eulerAngles;
        }
    }


}
