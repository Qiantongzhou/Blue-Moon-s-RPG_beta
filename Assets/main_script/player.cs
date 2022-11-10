using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

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

    gamesaving gamesaving;
    
    void Start()
    {
         currenthealth=healthpoint;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takedamage(int dam)
    {
        print("playertakedamage: " + dam);
        currenthealth-=dam;
    }
    public int getcurrenthealth()
    {
        return currenthealth;
    }
}
