using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPickup : MonoBehaviour
{
    // Reference to the GameManager or any object that handles the coin count
    public int coinText;
    

    // When the character collides with a coin, pick it up
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the coin
        if (other.CompareTag("Wizard"))
        {
             // Increment the player's coin count
            int coinText = PlayerPrefs.GetInt("Coins: ", 0);
            coinText ++;

            // Save the new coin count
            PlayerPrefs.SetInt("Coins: ", coinText);

            // Destroy the coin
            Destroy(gameObject);
        }
    }
}