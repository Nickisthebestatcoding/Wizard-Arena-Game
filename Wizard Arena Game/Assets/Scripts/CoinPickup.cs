using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // Reference to the GameManager or any object that handles the coin count
    public GameManager gameManager;
    

    // When the character collides with a coin, pick it up
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collided with the coin
        if (other.CompareTag("Wizard"))
        {
            // Increase the coin count in the GameManager
            gameManager.AddCoin();

            // Destroy the coin after it's picked up
            Destroy(gameObject);
        }
    }
}