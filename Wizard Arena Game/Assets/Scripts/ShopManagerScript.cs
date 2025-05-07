using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public TextMeshProUGUI CoinsTXT;


    public int healthFlaskCount = 0; // Track the number of health flasks
    public bool[] spellsUnlocked = new bool[5]; // Track unlocked spells

    void Start()
    {
        CoinsTXT.text = "Coins:" + coins;

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

        // Check if the player has enough coins to buy the item
        if (coins >= shopItems[2, itemID])
        {
            // Deduct the cost from coins
            coins -= shopItems[2, itemID];
            shopItems[3, itemID]++;

            // Update UI
            CoinsTXT.text = "Coins: " + coins;
            ButtonRef.GetComponent<Buttoninfo>().QuantityTxt.text = shopItems[3, itemID].ToString();

            // If the health flask is bought, increase its count
            if (itemID == 1)
            {
                healthFlaskCount++;
            }

            // Unlock spells when bought
            if (itemID == 2)
            {
                spellsUnlocked[2] = true; // Unlock Ice Bullet
            }
            if (itemID == 3)
            {
                spellsUnlocked[3] = true; // Unlock Tornado
            }
            if (itemID == 4)
            {
                spellsUnlocked[4] = true; // Unlock Lightning
            }
        }
    }
}
