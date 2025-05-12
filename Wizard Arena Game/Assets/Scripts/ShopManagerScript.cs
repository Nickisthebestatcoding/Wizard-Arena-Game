using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public static ShopManagerScript Instance;  // Singleton pattern
    public int[,] shopItems = new int[5, 5];
    public TextMeshProUGUI CoinsTXT;

    public int healthFlaskCount = 0; // Track the number of health flasks
    public int IceBulletCount = 0;
    public int TornadoCount = 0;
    public int LightningCount = 0;
    public bool[] spellsUnlocked = new bool[5]; // Track unlocked spells

    void Awake()
    {
        // Ensure Singleton instance
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        CoinsTXT.text = "Coins:" + WizardCoinManager.Instance.GetCoins();

        // ID's for shop items
        shopItems[1, 1] = 1; // Health Flask
        shopItems[1, 2] = 2; // Ice Bullet
        shopItems[1, 3] = 3; // Tornado
        shopItems[1, 4] = 4; // Lightning

        // Prices of items
        shopItems[2, 1] = 3;  // Health Flask cost
        shopItems[2, 2] = 10; // Ice Bullet cost
        shopItems[2, 3] = 20; // Tornado cost
        shopItems[2, 4] = 30; // Lightning cost

        // Quantity of items
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemID = ButtonRef.GetComponent<Buttoninfo>().ItemID;
        int itemCost = shopItems[2, itemID];

        // Check if the player has enough coins to buy the item
        if (WizardCoinManager.Instance.SpendCoins(itemCost))
        {
            shopItems[3, itemID]++;
            CoinsTXT.text = "Coins:" + WizardCoinManager.Instance.GetCoins();
            ButtonRef.GetComponent<Buttoninfo>().QuantityTxt.text = shopItems[3, itemID].ToString();

            // If spells are bought, unlock them
            if (itemID == 2)
                spellsUnlocked[2] = true; // Unlock Ice Bullet
            if (itemID == 3)
                spellsUnlocked[3] = true; // Unlock Tornado
            if (itemID == 4)
                spellsUnlocked[4] = true; // Unlock Lightning
        }
    }

    private void Update()
    {
        CoinsTXT.text = "Coins:" + WizardCoinManager.Instance.GetCoins();
    }
}
