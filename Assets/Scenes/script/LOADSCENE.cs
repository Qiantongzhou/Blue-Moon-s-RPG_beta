using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOADSCENE : MonoBehaviour
{
    // Start is called before the first frame update

    public void changesenceto(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
  
}
