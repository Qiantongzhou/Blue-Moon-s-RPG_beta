using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static UnityEngine.Rendering.DebugUI;

public class player : MonoBehaviour
{



    public Attributes attr;





    private int currenthealth;

    private int currentmagicpoint;


    public int damageblock { get; set; }

    public int attackdamage { get; set; }
    public int attackdamagebonus { get; set; }

    public int critdamage { get; set; }
    public int critchance { get; set; }

    public int magicdamage { get; set; }


    public int attackrange { get; set; }
    public int attackspeed { get; set; }

    public int movespeed { set; get; }
    public int turnrate;

    int gems;
    int gold;

    gamesaving gamesaving;
    Canvas canvas;
    float timecaculate;
    void Start()
    {
        gamesaving = GameObject.Find("gamesaving").GetComponent<gamesaving>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        attr = GetComponent<Attributes>();
        currenthealth = attr.healthpoint;
        currentmagicpoint = attr.magicpoint;
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
       
        TMP_Text[] j = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<TMP_Text>();
        j[3].text = gems.ToString();
        j[4].text = gold.ToString();


        float value = (float)getcurrenthealth() / attr.healthpoint;
        float magic = (float)getcurrentmagic() /  attr.magicpoint;

        Slider[] y = canvas.GetComponentsInChildren<Slider>();
        y[0].value = value;
        y[1].value = magic;
        TMP_Text[] x= canvas.GetComponentsInChildren<TMP_Text>();
        x[0].text = getcurrenthealth() + "/" + attr.healthpoint;
        x[1].text = getcurrentmagic() + "/" + attr.magicpoint;
        if (attr.attackdamagebonus > 0)
        {
            x[5].text = attr.attackdamage.ToString() + "<color=green>+" + attr.attackdamagebonus.ToString() + "</color>";
        }
        if (attr.attackdamagebonus < 0)
        {
            x[5].text = attr.attackdamage.ToString() + "<color=red>+" + attr.attackdamagebonus.ToString() + "</color>";
        }
        if (attr.attackdamagebonus == 0)
        {
            x[5].text = attr.attackdamage.ToString();
        }
        x[6].text = attr.critdamage.ToString();
        x[7].text = attr.damageblock.ToString();
    }
    private void FixedUpdate()
    {
        gems = int.Parse(gamesaving.getGameSavingGems());
        healthregenpersec();
    }
    private void healthregenpersec()
    {
        if (currenthealth < attr.healthpoint)
        {
            timecaculate += Time.deltaTime;
            if (timecaculate > 1.0f)
            {
                currenthealth = currenthealth + attr.healthregen;
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

    public Attributes GetAttributes()
    {
        return attr;
    }

    public void SetAttributes(Attributes newAttr)
    {
        attr = newAttr;
    }

    public void IncreaseAttributes(Attributes newAttr)
    {
        attr += newAttr;
    }
}
