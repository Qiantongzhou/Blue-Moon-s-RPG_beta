using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopprefab;
    GameObject canvas;
    GameObject shops;
    bool open;
    void Start()
    {
       canvas =GameObject.Find("Canvas");
        open = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) &&open==false)
        {
            print(open);
            invokeshop();
            open = true;
        }else
        if(Input.GetKeyDown(KeyCode.E) &&open==true)
        {
            print(open);
            Destroy(shops);
            open = false;
        }

    }

    public void invokeshop()
    {
       shops=Instantiate(shopprefab,canvas.transform);
        shops.transform.SetAsLastSibling();
    }
}
