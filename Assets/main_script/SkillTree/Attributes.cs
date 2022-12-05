using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attributes : MonoBehaviour
{

    public int healthpoint;

    public int healthregen;

    public int magicpoint;


    public int damageblock;

    public int attackdamage;
    public int attackdamagebonus;

    public int critdamage;
    public int critchance;

    public int magicdamage;


    public int attackrange;
    public int attackspeed;

    public int movespeed;

    static public Attributes operator +(Attributes attr, Attributes other)
    {
        attr.healthpoint += other.healthpoint;
        attr.healthregen += other.healthregen;
        attr.magicpoint +=  other.magicpoint;
        attr.damageblock += other.damageblock;
        attr.attackdamage += other.attackdamage;
        attr.attackdamagebonus += other.attackdamagebonus;
        attr.critdamage += other.critdamage;
        attr.critchance += other.critchance;
        attr.magicdamage += other.magicdamage;
        attr.attackrange += other.attackrange;
        attr.attackspeed += other.attackrange;
        attr.movespeed += other.movespeed;

        return attr;
    }
    
}
