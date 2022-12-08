using UnityEngine;

public class MaterialColorOverride : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }
}
