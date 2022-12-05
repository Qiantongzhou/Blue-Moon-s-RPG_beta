using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class uicontroller : MonoBehaviour
{
    public GameObject wavetimer;
    public GameObject wavedisplay;
    public AudioClip Clip;
    GameObject timer;
    GameObject wave;
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


        wave = Instantiate(wavedisplay,transform);
        wave.transform.SetAsLastSibling();
        wave.gameObject.SetActive(false);
        wave.transform.AddComponent<AudioSource>().clip=Clip;
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
                StartCoroutine(displaywave());
            }
            timer.GetComponent<Slider>().value = (waveinterval - wavetimenow) / waveinterval;
            timer.transform.GetChild(3).GetComponent<TMP_Text>().text =Mathf.FloorToInt(wavetimenow).ToString();
        }
        
        
    }

    IEnumerator displaywave()
    {
        wave.transform.GetChild(4).GetComponent<TMP_Text>().text ="WAVE: " +(DamageCalculator.currentwave+1).ToString();
        wave.transform.GetComponent<AudioSource>().Play();
        wave.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        wave.gameObject.SetActive(false);
    }
}
