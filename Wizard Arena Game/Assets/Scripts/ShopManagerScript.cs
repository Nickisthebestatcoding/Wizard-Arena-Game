using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // Added for EventSystem

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public TextMeshProUGUI CoinsTXT;

    public int healthFlaskCount = 0; // Track how many health flasks the player has
    public bool[] spellsUnlocked = new bool[5]; // Track which spells are unlocked (index 0 for Fireball, etc.)

    void Start()
    {
        CoinsTXT.text = "Coins:" + coins;

        // IDs for items
        shopItems[1, 1] = 1;  // Health Flask
        shopItems[1, 2] = 2;  // Ice Bullet
        shopItems[1, 3] = 3;  // Tornado
        shopItems[1, 4] = 4;  // Lightning

        // Prices
        shopItems[2, 1] = 5;  // Price for health flask
        shopItems[2, 2] = 10; // Price for ice bullet
        shopItems[2, 3] = 20; // Price for tornado
        shopItems[2, 4] = 30; // Price for lightning

        // Quantities (initially 0)
        shopItems[3, 1] = 0; // Health flask quantity
        shopItems[3, 2] = 0; // Ice bullet quantity
        shopItems[3, 3] = 0; // Tornado quantity
        shopItems[3, 4] = 0; // Lightning quantity

        // Set initial unlocked spells (you can modify based on the game design)
        spellsUnlocked[0] = true; // Fireball is always unlocked
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonRef.GetComponent<Buttoninfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<Buttoninfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<Buttoninfo>().ItemID]++;
            CoinsTXT.text = "Coins:" + coins;

            // If we bought a health flask, increment the flask count
            if (ButtonRef.GetComponent<Buttoninfo>().ItemID == 1)
            {
                healthFlaskCount++;
            }

            // If a spell is bought, unlock it (you can change this based on which item is being bought)
            if (ButtonRef.GetComponent<Buttoninfo>().ItemID == 2) // Ice Bullet
            {
                spellsUnlocked[2] = true;
            }
            if (ButtonRef.GetComponent<Buttoninfo>().ItemID == 3) // Tornado
            {
                spellsUnlocked[3] = true;
            }
            if (ButtonRef.GetComponent<Buttoninfo>().ItemID == 4) // Lightning
            {
                spellsUnlocked[4] = true;
            }

            ButtonRef.GetComponent<Buttoninfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<Buttoninfo>().ItemID].ToString();
        }
    }
}
