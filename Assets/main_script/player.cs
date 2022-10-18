using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public int healthpoint;
    public int healthregen;
    public int magicpoint;
    public int damageblock;

    public int attackdamage;
    public int magicdamage;
    public int attackrange;
    public int attackspeed;

    gamesaving gamesaving;
    sheshouanime x;
    void Start()
    {
        healthpoint = 100;

        healthregen = 100;
        magicpoint = 100;
        damageblock = 100;
        attackdamage = 100;
        damageblock = 100;
        attackspeed = 1;
        magicdamage = 100;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
