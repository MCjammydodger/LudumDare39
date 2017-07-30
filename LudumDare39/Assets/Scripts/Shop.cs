using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    public delegate void OnItemPurchased();

    [SerializeField]
    private ShopItemButton shopItemPrefab;
    [SerializeField]
    private Transform itemList;
    [SerializeField]
    private Text scoreText;

    private GameManager gameManager;

    private ShopItem[] shopItems;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        shopItems = new ShopItem[] {
            new ShopItem("Rocket", gameManager.GetRocketPrice(), gameManager.SetRocketActive),
            new ShopItem("Extra jetpack fuel", gameManager.GetFuelPrice(), gameManager.IncreaseFuel),
            new ShopItem("Forcefield", gameManager.GetForcefieldPrice(), gameManager.EnableForcefield)

        };
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        RefreshShop();
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    public void RefreshShop()
    {
        foreach(Transform child in itemList)
        {
            Destroy(child.gameObject);
        }
        foreach(ShopItem item in shopItems)
        {
            ShopItemButton button = Instantiate(shopItemPrefab);
            button.transform.SetParent(itemList);
            if(gameManager.GetScore() < item.itemPrice)
            {
                item.locked = true;
            }
            else
            {
                item.locked = false;
            }
            button.Setup(item, this);
        }
        scoreText.text = "Score: " + Mathf.RoundToInt(gameManager.GetScore()).ToString();
    }
}

public class ShopItem
{
    public string itemName;
    public float itemPrice;
    public Shop.OnItemPurchased ItemPurchasedCallback;
    public bool purchased;
    public bool locked;

    public ShopItem (string name, float price, Shop.OnItemPurchased callback)
    {
        itemName = name;
        itemPrice = price;
        ItemPurchasedCallback = callback;
    }
}
