using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public enum AttributeName { healthpoint
                                , healthregen
                                , magicpoint
                                , damageblock
                                , attackdamage
                                , attackdamagebonus
                                , critdamage
                                , critchance
                                , magicdamage
                                , attackrange
                                , attackspeed
                                , movespeed}

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
        attr.attackspeed += other.attackspeed;
        attr.movespeed += other.movespeed;

        return attr;
    }

    public void SetZero()
    {
        healthpoint = 0;
        healthregen = 0;
        magicpoint = 0;
        damageblock = 0;
        attackdamage = 0;
        critdamage = 0;
        critchance = 0;
        attackdamagebonus = 0;
        magicdamage = 0;
        movespeed = 0;
        attackrange = 0;
        attackspeed = 0;
    }


    public void changeAttribute(AttributeName name, int value)
    {
        switch (name)
        {
            case AttributeName.healthpoint:
                healthpoint += value;
                break;
            case AttributeName.healthregen:
                healthregen += value;
                break;
            case AttributeName.magicpoint:
                magicpoint += value;
                break;
            case AttributeName.damageblock:
                damageblock += value;
                break;
            case AttributeName.attackdamage:
                attackdamage += value;
                break;
            case AttributeName.attackdamagebonus:
                attackdamagebonus += value;
                break;
            case AttributeName.critdamage:
                critdamage += value;
                break;
            case AttributeName.attackrange:
                attackrange += value;
                break;
            case AttributeName.critchance:
                critchance += value;
                break;  
            case AttributeName.magicdamage:
                magicdamage += value;
                break;
            case AttributeName.movespeed:
                movespeed += value;
                break;  
        }
    }
}
