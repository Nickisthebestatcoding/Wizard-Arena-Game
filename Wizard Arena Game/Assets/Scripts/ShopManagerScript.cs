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

    // Index 2-4 for unlockable spells (IceBullet = 2, Tornado = 3, Lightning = 4)
    public bool[] spellsUnlocked = new bool[5];

    void Start()
    {
        CoinsTXT.text = "Coins:" + coins;

        // IDs
        shopItems[1, 1] = 1; // Health Flask
        shopItems[1, 2] = 2; // Ice Bullet
        shopItems[1, 3] = 3; // Tornado
        shopItems[1, 4] = 4; // Lightning

        // Prices
        shopItems[2, 1] = 5;   // Health Flask
        shopItems[2, 2] = 10;  // Ice Bullet
        shopItems[2, 3] = 20;  // Tornado
        shopItems[2, 4] = 30;  // Lightning

        // Quantities
        for (int i = 1; i <= 4; i++)
            shopItems[3, i] = 0;

        // Fireball is default, so no need to unlock it
        // Health Flask is not a spell, so no unlock needed
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event")
            .GetComponent<EventSystem>()
            .currentSelectedGameObject;

        int itemID = ButtonRef.GetComponent<Buttoninfo>().ItemID;
        int price = shopItems[2, itemID];

        if (coins >= price)
        {
            coins -= price;
            shopItems[3, itemID]++;
            CoinsTXT.text = "Coins:" + coins;
            ButtonRef.GetComponent<Buttoninfo>().QuantityTxt.text = shopItems[3, itemID].ToString();

            // Only unlock if it's a spell (IDs 2–4)
            if (itemID >= 2 && itemID <= 4)
                spellsUnlocked[itemID] = true;
        }
    }
}
