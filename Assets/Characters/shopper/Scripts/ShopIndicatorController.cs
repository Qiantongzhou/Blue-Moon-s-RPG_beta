using UnityEngine;

public class ShopIndicatorController : MonoBehaviour
{
    public GameObject Indicator;

    private GameObject indicatorInstance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Players.CurrentPlayer)
        {
            indicatorInstance = Instantiate(Indicator, GameObject.Find("Canvas").transform);
            indicatorInstance.transform.SetAsLastSibling(); ;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        { Destroy(indicatorInstance); }
    }
}
