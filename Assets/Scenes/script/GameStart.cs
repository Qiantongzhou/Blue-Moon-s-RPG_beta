using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gamesetting.difficulty = 1;
        gamesetting.mode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEasy()
    {
        gamesetting.difficulty = 1;
    }

    public void setHard()
    {
        gamesetting.difficulty = 2;

    }
    public void setimpossible()
    {
        gamesetting.difficulty = 3;
    }
    public void setgamemodeNormal()
    {
        gamesetting.mode = 1;
    }
    public void setgamemodeServival()
    {
        gamesetting.mode = 2;
    }

    public void loadArcherlevel()
    {
        SceneManager.LoadScene("archergame");
    }
    public void loadpaladinleve()
    {
        SceneManager.LoadScene("PaladinWinterGame");
    }
}
