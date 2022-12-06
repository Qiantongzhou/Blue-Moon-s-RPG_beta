using UnityEngine;

public class ShopIndicator : MonoBehaviour
{
    public GameObject ShopperMenuPrefab;

    private const string Button_Shop = "Shop";
    private GameObject canvas;
    private GameObject shopperMenuInstance;
    private bool isShopperMenuOpen;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        isShopperMenuOpen = false;
    }
    void Update()
    {
        if (Input.GetButtonDown(Button_Shop))
        {
            if (isShopperMenuOpen) { Destroy(shopperMenuInstance); }
            else { InstantiateShopperMenu(); }
            isShopperMenuOpen = !isShopperMenuOpen;
        }
    }

    public void InstantiateShopperMenu()
    {
        shopperMenuInstance = Instantiate(ShopperMenuPrefab, canvas.transform);
        shopperMenuInstance.transform.SetAsLastSibling();
    }
}
