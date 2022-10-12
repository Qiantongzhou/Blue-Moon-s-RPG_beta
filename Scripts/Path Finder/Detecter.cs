using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Detecter : MonoBehaviour
{
    private PathManager pathManager;
    public Gate[] gates;

    private string[] gateName;
    private int gateCounts;

    private void Awake()
    {   
        
        if(name.IndexOf(" ") == -1)
        {
            throw new System.Exception("Every path must be ended with \"space+number\"  所有寻路碰撞箱必须以空格加数字结尾");
        }
        pathManager = GetComponentInParent<PathManager>();
        gateCounts = gates.Length;
        if(pathManager.GetGateCount() !=0 && gateCounts == 0)
        {
            throw new System.Exception("Plain " + GetID() + " is not reachable, please check the gate mapping  平面" + GetID() + "不可抵达，请检查门连接设置");
        }
        gateName = new string[gateCounts];
        for(int i = 0; i < gateCounts; i++)
        {
            gateName[i] = gates[i].name;
        }
    }

    public string[] getGates()
    {
        return gateName;
    }
    private void OnTriggerStay(Collider other)
    {
        //print(gameObject.name + " -> OnTriggerStay: " + other.gameObject.name);
        if (IsPlayer(other)) return;
        if (IsEnemy(other)) return;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsEnemy(other)) return;
    }

    private bool IsPlayer(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   

            string playerin = this.name.Substring(name.IndexOf(" "));
            //print(playerin);
            pathManager.UpdatePlayerIn(playerin);
            return true;
        }
        return false;
    }
    private bool IsEnemy(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //print("updating");
            string enemyin = this.name.Substring(name.IndexOf(" "));
            //print(playerin);
            other.GetComponent<SimpleEnemy>().UpdateEnemyPos(enemyin);//need to change script
            return true;
        }
        return false;
    }
    public int GetID()
    {
        return int.Parse(name.Substring(name.IndexOf(" ")));
    }
}
