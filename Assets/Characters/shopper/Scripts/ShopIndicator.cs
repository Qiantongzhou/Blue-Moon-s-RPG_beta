using UnityEngine;

public class ShopIndicator : MonoBehaviour
{
    public GameObject ShopperMenuPrefab;

    private const string Button_Shop = "Shop";

    private GameObject shopperMenuInstance;
    private bool isShopperMenuOpen;

    void Start()
    {
     
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
        shopperMenuInstance = Instantiate(ShopperMenuPrefab, transform);
        shopperMenuInstance.transform.SetAsLastSibling();
    }

}
