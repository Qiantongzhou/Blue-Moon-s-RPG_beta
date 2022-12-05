using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] All_NPC;
    public GameObject[] NPC_POS;
    public GameObject[] ENEMY_POS;
    public bool startwave=false;
    public float timebetweenwaves;
    public int enmeytospwan;
    public int currentenemy;
    void Start()
    {
        unity_diceng.NPC_GEN = All_NPC;
        unity_diceng.NPC_POS= NPC_POS;


        DamageCalculator.multiPerEnemy = 1.0f;
        DamageCalculator.multiPerElite = 1.0f;
        DamageCalculator.maxcreatureCount = 30;
        DamageCalculator.currentwave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startwave)
        {
            StartCoroutine(startfirstwave());
        }
    }
    IEnumerator startfirstwave()
    {
        startwave = true;
    yield return new WaitForSeconds(timebetweenwaves);
        DamageCalculator.currentwave++;
        print("wave"+DamageCalculator.currentwave);
        StartCoroutine(enmeyspawn());
        endwave();
    }
    public void endwave()
    {
        
        
        

       StartCoroutine(beginwave());
    }
    IEnumerator beginwave()
    {
        yield return new WaitForSeconds(timebetweenwaves);
        DamageCalculator.currentwave++;
        print("wave" + DamageCalculator.currentwave);
        StartCoroutine(enmeyspawn());
        endwave();
    }
    IEnumerator enmeyspawn()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentenemy != enmeytospwan)
        {
           currentenemy++;
            Instantiate(All_NPC[1], ENEMY_POS[0].transform.position, Quaternion.identity);
           StartCoroutine(enmeyspawn());
        }
        else
        {
            currentenemy = 0;
            print("waveend");
        }
       
    }

}
