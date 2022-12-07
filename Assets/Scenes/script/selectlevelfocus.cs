using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectlevelfocus : MonoBehaviour
{
    public void SelectNormal()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        gamesetting.mode = 0;
    }
    public void Selectservive()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        gamesetting.mode = 1;
    }

    public void SelectEasy()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        gamesetting.difficulty = 0;
    }
    public void SelectHard()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        gamesetting.difficulty = 1;
    }
    public void SelectImpossible()
    {
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        gamesetting.difficulty = 2;
    }

}
