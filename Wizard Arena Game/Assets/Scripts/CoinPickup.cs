using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    // How much value the coin gives when picked up

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            // Increase player's coin count
            WizardCoinManager.Instance.AddCoins(coinValue);

            // Destroy the coin after being collected
            Destroy(gameObject);
        }
    }
   
  
}