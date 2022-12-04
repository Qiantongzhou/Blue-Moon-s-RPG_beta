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
    public int attackdamagebonus;

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
    Canvas canvas;
    float timecaculate;
    void Start()
    {
        gamesaving=GameObject.Find("gamesaving").GetComponent<gamesaving>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        currenthealth =healthpoint;
         currentmagicpoint=magicpoint;
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
       
        TMP_Text[] j = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<TMP_Text>();
        j[3].text = gems.ToString();
        j[4].text = gold.ToString();


        float value = (float)getcurrenthealth() /healthpoint;
        float magic = (float)getcurrentmagic() / magicpoint;

        Slider[] y = canvas.GetComponentsInChildren<Slider>();
        y[0].value = value;
        y[1].value = magic;
        TMP_Text[] x= canvas.GetComponentsInChildren<TMP_Text>();
        x[0].text = getcurrenthealth() + "/" + healthpoint;
        x[1].text = getcurrentmagic() + "/" + magicpoint;
        if (attackdamagebonus > 0)
        {
            x[5].text = attackdamage.ToString() + "<color=green>+" + attackdamagebonus.ToString() + "</color>";
        }
        if (attackdamagebonus < 0)
        {
            x[5].text = attackdamage.ToString() + "<color=red>+" + attackdamagebonus.ToString() + "</color>";
        }
        if (attackdamagebonus == 0)
        {
            x[5].text = attackdamage.ToString();
        }
        x[6].text = critdamage.ToString();
        x[7].text = damageblock.ToString();
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
