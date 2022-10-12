using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private int PlayerIn;
    private int temp;

    private short pathChanged = 0;//A count stamp to manage history path changed
    private Gate[] gates;
    private Detecter[] planes;
    private int gatesCount;
    private int planesCount;

    private void Awake()
    {
        Debug.LogWarning("map the gates in counterclockwise to have better performance");
        planes = GetComponentsInChildren<Detecter>();
        gates = GetComponentsInChildren<Gate>();
        temp = 0;
        gatesCount = gates.Length;
        planesCount = planes.Length;
        //print(GetDetecterCount());
    }

    // Update is called once per frame
    void Update()
    {   

        if(temp != PlayerIn)
        {
            pathChanged++;
            temp = PlayerIn;
        }
        //print(PlayerIn);
    }

    public void UpdatePlayerIn(int place)
    {
        PlayerIn = place;
    }
    public void UpdatePlayerIn(string place)
    {   
        PlayerIn = int.Parse(place);
    }
    public int GetPlayerIn()
    {
        return PlayerIn;
    }
    public short GetPathStamp()
    {
        return pathChanged;
    }

    public Gate GetGate(string name)
    {
        return gates[int.Parse(name.Substring(name.IndexOf(" ")))-1];
    }
    public Detecter GetDetecter(string name)
    {
        return planes[int.Parse(name.Substring(name.IndexOf(" ")))-1];
    }
    public int GetDetecterCount()
    {
        return planesCount;
    }
    public int GetGateCount()
    {
        return gatesCount;
    }
    public Vector3 FindPos(string pathName)
    {
        if(pathName.Substring(0,pathName.IndexOf(" ")).Equals("plain"))
        {
            return planes[int.Parse(pathName.Substring(pathName.IndexOf(" "))) - 1].transform.position;
        }
        else
        {
            return gates[int.Parse(pathName.Substring(pathName.IndexOf(" "))) - 1].transform.position;
        }
    }
}
