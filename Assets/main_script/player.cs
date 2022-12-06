using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.DebugUI;

public class player : MonoBehaviour
{
    
    public int healthpoint;
    private int currenthealth;

    public int healthregen;

    public int magicpoint;
    private int currentmagicpoint;


    public int damageblock;

    public int attackdamage;

    public int critdamage;
    public int critchance;

    public int magicdamage;


    public int attackrange;
    public int attackspeed;

    public int movespeed;
    public int turnrate;

    int gems;
    int gold;

    gamesaving gamesaving;
    float timecaculate;
    void Start()
    {
        gamesaving=GameObject.Find("gamesaving").GetComponent<gamesaving>();
         currenthealth=healthpoint;
         currentmagicpoint=magicpoint;
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {       
        TMP_Text[] x = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<TMP_Text>();
        x[3].text = gems.ToString();
        x[4].text = gold.ToString();
    }
    private void FixedUpdate()
    {
        gems = int.Parse(gamesaving.getGameSavingGems());
        healthregenpersec();
    }
    private void healthregenpersec()
    {
        if (currenthealth < healthpoint)
        {
            timecaculate += Time.deltaTime;
            if (timecaculate > 1.0f)
            {
                currenthealth = currenthealth + healthregen;
                timecaculate = 0.0f;
            }
        }
    }
    public void takedamage(int dam)
    {
        print("playertakedamage: " + dam);
        currenthealth-=dam;
        if(currenthealth < 0)
        {
            currenthealth = 0;
        }
    }
    public int getcurrenthealth()
    {
        return currenthealth;
    }
    public int getcurrentmagic()
    {
        return currentmagicpoint;
    }
}
