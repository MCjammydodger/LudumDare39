using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour {

    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text priceText;

    private ShopItem shopItem;
    private Shop shopObject;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Setup(ShopItem item, Shop shop)
    {
        shopItem = item;
        shopObject = shop; 
        itemName.text = item.itemName;
        if (item.purchased)
        {
            priceText.text = "Purchased";
            GetComponent<Button>().interactable = false;
        }
        else
        {
            priceText.text = "Price: " + item.itemPrice.ToString();
            if (item.locked)
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }
        
    }

    public ShopItem GetShopItem()
    {
        return shopItem;
    }

    public void OnButtonClicked()
    {
        shopItem.purchased = true;
        gameManager.AddScore(-shopItem.itemPrice);
        shopObject.RefreshShop();
        shopItem.ItemPurchasedCallback();
    }
}
