using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{

    public int[,] shopItems = new int[5,5];
    public float coins;
    public TextMeshProUGUI CoinsTXT;

    void Start()
    {
        CoinsTXT.text = "Coins:" + coins;

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Price
        shopItems[2, 1] = 5;
        shopItems[2, 2] = 15;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 30;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }

    
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonRef.GetComponent<Buttoninfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<Buttoninfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<Buttoninfo>().ItemID]++;
            CoinsTXT.text = "Coins:" + coins;
            ButtonRef.GetComponent<Buttoninfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<Buttoninfo>().ItemID].ToString();
        }

    }
}
