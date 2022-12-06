using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class displaytownhealth : MonoBehaviour
{
    Slider Slider;
    TMP_Text health;
    void Start()
    {
        Slider = GetComponent<Slider>();
        health =transform.GetChild(1).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value = (float)gamestatistics.currenthealth / gamestatistics.maxhealth;
        health.text = gamestatistics.currenthealth.ToString() + "/" + gamestatistics.maxhealth.ToString();
    }
}
