using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public static ShopManagerScript Instance;

    public int[,] shopItems = new int[5, 5];
    public TextMeshProUGUI CoinsTXT;

    public int healthFlaskCount = 0;
    public int IceBulletCount = 0;
    public int TornadoCount = 0;
    public int LightningCount = 0;

    public bool[] spellsUnlocked = new bool[5];

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        CoinsTXT.text = "Coins:" + WizardCoinManager.Instance.GetCoins();

        shopItems[1, 1] = 1; // Health Flask
        shopItems[1, 2] = 2; // Ice Bullet
        shopItems[1, 3] = 3; // Tornado
        shopItems[1, 4] = 4; // Lightning

        shopItems[2, 1] = 3;
        shopItems[2, 2] = 10;
        shopItems[2, 3] = 20;
        shopItems[2, 4] = 30;

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

        if (WizardCoinManager.Instance.SpendCoins(itemCost))
        {
            shopItems[3, itemID]++;
            CoinsTXT.text = "Coins:" + WizardCoinManager.Instance.GetCoins();
            ButtonRef.GetComponent<Buttoninfo>().QuantityTxt.text = shopItems[3, itemID].ToString();

            switch (itemID)
            {
                case 1:
                    healthFlaskCount++;
                    break;
                case 2:
                case 3:
                case 4:
                    UnlockSpell(itemID);
                    break;
            }
        }
    }

    public void UnlockSpell(int spellIndex)
    {
        // Only unlocks the specific spell, nothing else.
        if (spellIndex >= 0 && spellIndex < spellsUnlocked.Length)
        {
            spellsUnlocked[spellIndex] = true;
        }
    }

    private void Update()
    {
        CoinsTXT.text = "Coins:" + WizardCoinManager.Instance.GetCoins();
    }
}
