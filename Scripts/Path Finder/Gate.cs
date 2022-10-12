using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    //Warning: this gate can only attach 2 plains only in static map
    //警告！这个连接器定位点只能在静态地图中用于连接两个平面

    public Detecter detecter1;
    public Detecter detecter2;
    private string[] detecters = new string[2];
    public string[] getDetecter()
    {
        return detecters;
    }
    private void Awake()
    {
        if (name.IndexOf(" ") == -1)
        {
            throw new System.Exception("Every path must be ended with \"space+number\"  所有寻路碰撞箱必须以空格加数字结尾");
        }
        if (detecter1==null|| detecter2 == null)
        {
            throw new System.NullReferenceException("Every detecters must be assigned  门连接的两个寻路触发器不能为空");
        }
    }
    private void Start()
    {
        detecters[0] = detecter1.name;
        detecters[1] = detecter2.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (IsPlayer(other)) return;
        if (IsEnemy(other)) return;
    }
    private bool IsEnemy(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<SimpleEnemy>().StepIn();//need to change script

            return true;
        }
        return false;
    }
    

    /*private bool IsDetecter(Collider other)
    {
        if (other.gameObject.CompareTag("PathField"))
        {

            string plain = other.name;
            if (detecters[0] == null)
            {
                detecters[0] = plain;
                
            }
            else if (detecters[1]==null)
            {
                detecters[1] = plain;
                
            }
            
            return true;
        }
        return false;
    }*/
    private bool IsPlayer(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print(detecters[0]);
            return true;
        }
        return false;
    }

    public int GetID()
    {
        return int.Parse(name.Substring(name.IndexOf(" ")));
    }
}
