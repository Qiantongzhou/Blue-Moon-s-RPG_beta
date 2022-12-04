using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransfer : MonoBehaviour
{

    public static PlayerTransfer instance;
    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

}
