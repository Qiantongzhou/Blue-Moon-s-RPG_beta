using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class uicontroller : MonoBehaviour
{
    public GameObject wavetimer;
    GameObject timer;
    float currenttime;
    GameEngine gameEngine;
    float waveinterval;
    float wavetimenow;
    void Start()
    {
        gameEngine = GameObject.Find("GameEngine").GetComponent<GameEngine>();
        waveinterval = gameEngine.timebetweenwaves;
        wavetimenow = waveinterval;
        timer=Instantiate(wavetimer,transform);
        timer.transform.SetAsLastSibling();
        timer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEngine.startwave)
        {
            timer.SetActive(true);
            currenttime +=Time.deltaTime;
            wavetimenow-=Time.deltaTime;
            if(wavetimenow <= 0)
            {
                wavetimenow = waveinterval;
            }
            timer.GetComponent<Slider>().value = (waveinterval - wavetimenow) / waveinterval;
            timer.transform.GetChild(3).GetComponent<TMP_Text>().text =Mathf.FloorToInt(wavetimenow).ToString();
        }
        
        
    }
}
