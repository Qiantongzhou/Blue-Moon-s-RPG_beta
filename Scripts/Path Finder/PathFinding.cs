using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PathFinding : MonoBehaviour
{
    private enum label { unexplored, visited, discovery, back };
    public Transform followTarget;
    public PathManager p;
    public float speed = 2.5f;
    private int origin;
    private Vector3 dest;
    private Stack<string> pathSequence;
    private label[] PlainVisited;
    private label[] GateVisited;
    private const string plain = "plain";
    private const string gate = "gate";
    private int pathStamp = 0;
    private int step = 1;
    private string[] stepSequence;
    private string currentCenter;


    private void Awake()
    {
        pathSequence = new Stack<string>();
        //can be optimized by using random enemy generation
        //stepSequence = new string[1]; 
    }
    // Start is called before the first frame update
    void Start()
    {
        //print(p);
        PlainVisited = new label[p.GetDetecterCount()];
        GateVisited = new label[p.GetGateCount()];
        //print(p.GetDetecterCount());
        Set0();

        if (p.GetPlayerIn() > 0)
        {
            stepSequence = CalDest(p.GetPlayerIn());
            pathStamp = p.GetPathStamp();
        }
    }

    //This update is only for testing
    /*void Update()
    {
        //print(origin);
        //print(p.GetPathStamp() + " " + pathStamp+" "+ step);
        if (pathStamp != p.GetPathStamp())
        {
            callWhenPathChange();
        }
        if (origin != p.GetPlayerIn())
        {
            //print(stepSequence[0]);
            //print(stepSequence.Length+ " "+ step);
            if (stepSequence != null)
            {
                dest = p.FindPos(stepSequence[step]);
            }
        }
        else
        {
            dest = followTarget.position;
        }
        transform.position += speed * Time.deltaTime * (dest - transform.position).normalized;
        
    }*/

    public Vector3 CallWhenUpdate()
    {
        if (pathStamp != p.GetPathStamp())
        {
            CallWhenPathChange();
        }
        if (origin != p.GetPlayerIn())
        {
            //print(stepSequence[0]);
            //print(stepSequence.Length+ " "+ step);
            if (stepSequence != null)
            {
                return p.FindPos(stepSequence[step]);
            }
            return transform.position;
        }
        else
        {
            return followTarget.position;
        }
    }

    public void CallWhenPathChange()
    {
        Set0();

        stepSequence = CalDest(p.GetPlayerIn());
        pathStamp = p.GetPathStamp();
        step = 1;
    }
    private string[] CalDest(int dest)
    {
        CalDest(origin, dest);
        Stack<string> temp = new Stack<string>();
        while (pathSequence.Count > 0)
        {
            temp.Push(pathSequence.Pop());
        }
        return temp.ToArray();
    }
    private void CalDest(int current, int dest)
    {
        Detecter d;
        //print(current+" "+ p.GetPlayerIn());
        PlainVisited[current - 1] = label.visited;

        pathSequence.Push(plain + " " + current);
        if (current == dest)
        {
            return;
        }
        foreach (Gate g in p.GetDetecter(plain + " " + current).gates)
        {
            if (GateVisited[g.GetID() - 1] == label.unexplored)
            {
                if (g.detecter1.name.Equals(pathSequence.Peek()))
                {
                    d = g.detecter2;
                }
                else
                {
                    d = g.detecter1;
                }
                if (PlainVisited[d.GetID() - 1] == label.unexplored)
                {
                    GateVisited[g.GetID() - 1] = label.discovery;
                    pathSequence.Push(gate + " " + g.GetID());
                    CalDest(d.GetID(), dest);
                    if (pathSequence.Peek().Substring(0, pathSequence.Peek().IndexOf(" ")).Equals(gate))
                    {
                        pathSequence.Pop();
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    GateVisited[g.GetID() - 1] = label.back;
                }
            }
        }
        pathSequence.Pop();
        return;
    }


    private void Set0()
    {
        for (int i = 0; i < p.GetDetecterCount(); i++)
        {
            PlainVisited[i] = label.unexplored;
        }
        for (int i = 0; i < p.GetGateCount(); i++)
        {
            GateVisited[i] = label.unexplored;
        }
    }


    public void UpdateEnemyPos(int pos)
    {
        origin = pos;
    }

    public void UpdateEnemyPos(string enemyin)
    {
        origin = int.Parse(enemyin);
    }

    public void StepIn()
    {
        step++;
    }
}
