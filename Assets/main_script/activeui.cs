using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class activeui : MonoBehaviour
{
  public void activechild(int number)
    {
        transform.GetChild(number).gameObject.SetActive(true);
    }
    public void inactiveparent()
    {
        transform.parent.gameObject.SetActive(false);
    }
    public void inactiveparentlevel3()
    {
        transform.parent.parent.parent.gameObject.SetActive(false);
    }
}
