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
    public float firstwavetime;
    public float timebetweenwaves;
    public int enmeytospwan;
    public int currentenemy;
    public int Enemymultiper;
    public GameObject missionfailed;
    bool lose = false;
    void Awake()
    {
        unity_diceng.NPC_GEN = All_NPC;
        unity_diceng.NPC_POS= NPC_POS;


        DamageCalculator.multiPerEnemy = Enemymultiper;
        DamageCalculator.multiPerElite = Enemymultiper;
        DamageCalculator.maxcreatureCount = 30;
        DamageCalculator.currentwave = 0;




        if (gamesetting.difficulty == 0)
        {
            firstwavetime = 100;
            timebetweenwaves = 60;
            DamageCalculator.multiPerEnemy = 1;
            DamageCalculator.multiPerElite = 1;
        }
        if (gamesetting.difficulty == 1)
        {
            firstwavetime = 60;
            timebetweenwaves = 50;
            DamageCalculator.multiPerEnemy = 2;
            DamageCalculator.multiPerElite = 2;
        }
        if (gamesetting.difficulty ==2)
        {
            firstwavetime = 30;
            timebetweenwaves = 30;
            DamageCalculator.multiPerEnemy = 5;
            DamageCalculator.multiPerElite = 5;
        }

        if(gamesetting.mode ==1)
        {
            enmeytospwan = 20;
            
        }
        if (gamesetting.mode == 0)
        {
            enmeytospwan = 10;

        }




    }

    // Update is called once per frame
    void Update()
    {
        if (startwave)
        {
            GameObject.Find("Canvas").GetComponent<uicontroller>().startwave = true;
            StartCoroutine(startfirstwave());
        }
        if (!lose)
        {
            checkwin();
        }
       

    }
    IEnumerator startfirstwave()
    {
        startwave = false;
    yield return new WaitForSeconds(firstwavetime);
        DamageCalculator.currentwave++;
        print("wave"+DamageCalculator.currentwave);
        StartCoroutine(enmeyspawn());
        endwave();
    }
    public void checkwin()
    {
        if (DamageCalculator.currentwave > 10)
        {
        }
        if (gamestatistics.housecount == 0)
        {
            GameObject.Find("Canvas").GetComponent<uicontroller>().startwave = false;
            gameover();
        }
    }

    public void gameover()
    {
        Instantiate(missionfailed,GameObject.Find("Canvas").transform);
        lose = true;
    }
    public void endwave()
    {


        if (DamageCalculator.currentwave > 10)
        {
            Instantiate(All_NPC[1], ENEMY_POS[0].transform.position, Quaternion.identity);
        }
        else
        {

            StartCoroutine(beginwave());
        }
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
            int x = Random.Range(1, All_NPC.Length - 1);
            
            Instantiate(All_NPC[x], ENEMY_POS[0].transform.position, Quaternion.identity);
            StartCoroutine(enmeyspawn());
        }
        else
        {
            currentenemy = 0;
            print("waveend");
        }
       
    }

}
