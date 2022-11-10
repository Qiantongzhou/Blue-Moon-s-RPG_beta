using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

public class DamageCalculator
{
   

    //damage modifiers
    public static short enemyCount;
    //percentage stronger, product of current;
    public static float multiPerEnemy;


    public static short eliteCount;
    public static float multiPerElite;


    public static short maxEnemyCount;
    //public static BossType bossType;
    public static float conditionCounter;
    public static float conditionOverwhelm;

    //optional
    public static float levelGap;
    public static float maxLevelGap = 10;

    public static float playerSpeed;
    public static bool inAir;
    public static float currentLife;
    public static float maxLife;
    public static float criticalDamage;
    public static bool isCritial;
    public static float reductionPerSec;
    public static float reductionTime;
    public static float minimumReduction;

    public static float enemyLife;
    public static float eliteLife;
    public static float bossLife;
    public static float damageReduction;


    //damage functions    need GameTag Class
    //simple damage

    public static float TakeDamage(GameTag[] attacker, GameTag[] taker, float baseDamage)
    {
        float result = baseDamage;
        result *= attackerAnalyser(attacker);
        foreach (GameTag tag in taker)
        {

        }
        return result;
    }


    //attacker calculation

    private static float attackerAnalyser(GameTag[] attacker)
    {
        bool reduction = false;
        float result = 1;

        float enemyTargetMulti = 1;


        foreach (GameTag tag in attacker)
        {

            if(tag == GameTag.isCountered)
            {
                result *= conditionCounter;
            }
            if(tag == GameTag.isOverwhelmed)
            {
                result *= conditionOverwhelm;
            }

            if(tag == GameTag.careEnemyCount)
            {
                enemyTargetMulti += multiEnemyMultiplyer();
            }
            if(tag == GameTag.careEliteCount)
            {
                enemyTargetMulti += multiEliteMultiplyer();
            }
            if(tag == GameTag.careEnemyLife)
            {
                enemyTargetMulti += enemyLifeMulti();
            }
            if(tag == GameTag.careEliteLife)
            {
                enemyTargetMulti += eliteLifeMulti();
            }
            if(tag == GameTag.careBossLife)
            {
                enemyTargetMulti += bossLifeMulti();
            }

            if (tag == GameTag.isCriticable)
            {
                result *= attackerCritMulti()-1;
            }
            if (tag == GameTag.isRedution)
            {
                reduction = true;
            }

            if (tag == GameTag.careCurrentLife)
            {
                result *= healthMultiplyer();
            }

            if (reduction)
            {
                if(tag == GameTag.linearReduction)
                {
                    result *= linearReduction();
                }
                if(tag == GameTag.rootReduction)
                {
                    result *= rootReduction();
                }
            }


        }
        
        return result * enemyTargetMulti;
    }

    private static float multiEnemyMultiplyer()
    {
        return (float) Math.Pow(multiPerEnemy, enemyCount);
    }

    private static float multiEliteMultiplyer()
    {
        return (float)Math.Pow(multiPerElite, eliteCount);
    }

    private static float healthMultiplyer()
    {
        return 1/(currentLife / maxLife);
    }

    private static float attackerCritMulti()
    {
        return isCritial ?  criticalDamage:1;
    }

    private static float reduction(GameTag reductionType)
    {
        if (reductionType == GameTag.linearReduction)
        {
            return linearReduction();
        }
        if( reductionType == GameTag.rootReduction)
        {
            return rootReduction();
        }
        return 1;
    }

    private static float linearReduction()
    {
        return Math.Min((1 / reductionPerSec) * reductionTime, minimumReduction);
    }
    private static float rootReduction()
    {
        return Math.Min((float) Math.Pow( reductionTime, (1 / reductionPerSec)), minimumReduction);
    }

    private static float enemyLifeMulti()
    {
        return 1 - (1 / enemyLife);
    }

    private static float eliteLifeMulti()
    {
        return 1 - (1 / eliteLife);
    }
    private static float bossLifeMulti()
    {
        return 1 - (1 / bossLife);
    }



    //taker calculation




}
