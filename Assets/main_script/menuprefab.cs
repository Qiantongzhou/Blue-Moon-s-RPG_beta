using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuprefab : MonoBehaviour
{
    public GameObject canvas;
    GameObject temp;
    bool open;

    Animator anims;
    public void instantcanvas()
    {
        if (open == false)
        {
            stopinput();
            temp = Instantiate(canvas, GameObject.Find("Canvas").transform);
            temp.transform.SetAsLastSibling();
            open = true;
            
        }
        else
        {
            anims.SetBool("close", true);
            Destroy(temp,1);
            open = false;
            unstopinput();
        }
        anims = temp.GetComponent<Animator>();
    }


    public void stopinput()
    {
        GobalEvent.Pause_player_mouse_input = true;
    }
   public void unstopinput()
    {
        if (!open)
        {
            GobalEvent.Pause_player_mouse_input = false;
        }
    }

}
