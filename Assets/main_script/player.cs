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

    public Attributes equipAttr;
    public Attributes skillAttr;


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
        equipAttr = gameObject.AddComponent<Attributes>();
        skillAttr = gameObject.AddComponent<Attributes>();
        currenthealth = ResultAttr().healthpoint;
        currentmagicpoint = ResultAttr().magicpoint;
        gold = 0;
    }

    public Attributes ResultAttr()
    {
        Attributes temp;
        temp = attr + equipAttr;
        temp += skillAttr;
        return temp;
    }

    // Update is called once per frame
    void Update()
    {
        
       
        TMP_Text[] j = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponentsInChildren<TMP_Text>();
        j[3].text = gems.ToString();
        j[4].text = gold.ToString();


        float value = (float)getcurrenthealth() / ResultAttr().healthpoint;
        float magic = (float)getcurrentmagic() / ResultAttr().magicpoint;

        Slider[] y = canvas.GetComponentsInChildren<Slider>();
        y[0].value = value;
        y[1].value = magic;
        TMP_Text[] x= canvas.GetComponentsInChildren<TMP_Text>();
        x[0].text = getcurrenthealth() + "/" + ResultAttr().healthpoint;
        x[1].text = getcurrentmagic() + "/" + ResultAttr().magicpoint;
        if (ResultAttr().attackdamagebonus > 0)
        {
            x[5].text = ResultAttr().attackdamage.ToString() + "<color=green>+" + ResultAttr().attackdamagebonus.ToString() + "</color>";
        }
        if (ResultAttr().attackdamagebonus < 0)
        {
            x[5].text = ResultAttr().attackdamage.ToString() + "<color=red>+" + ResultAttr().attackdamagebonus.ToString() + "</color>";
        }
        if (ResultAttr().attackdamagebonus == 0)
        {
            x[5].text = ResultAttr().attackdamage.ToString();
        }
        x[6].text = ResultAttr().critdamage.ToString();
        x[7].text = ResultAttr().damageblock.ToString();
    }
    private void FixedUpdate()
    {
        gems = int.Parse(gamesaving.getGameSavingGems());
        healthregenpersec();
    }
    private void healthregenpersec()
    {
        if (currenthealth < ResultAttr().healthpoint)
        {
            timecaculate += Time.deltaTime;
            if (timecaculate > 1.0f)
            {
                currenthealth = currenthealth + ResultAttr().healthregen;
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

    public Attributes GetBaseAttributes()
    {
        return attr;
    }

    public Attributes GetEquipAttributes()
    {
        return equipAttr;
    }

    public Attributes GetSkillAttributes()
    {
        return skillAttr;
    }

    public void SetEquipAttributes(Attributes newAttr)
    {
        equipAttr = newAttr;
    }

    public void SetSkillAttributes(Attributes newAttr)
    {
        equipAttr = newAttr;
    }

    /*public void IncreaseAttributes(Attributes newAttr)
    {
        attr += newAttr;
    }*/
}
