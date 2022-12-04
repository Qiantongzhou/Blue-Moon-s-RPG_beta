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
            Debug.Log("Enter");
            PlayerTransfer.instance.transform.position = transform.position;
            PlayerTransfer.instance.transform.eulerAngles = transform.eulerAngles;
        }
    }


}
