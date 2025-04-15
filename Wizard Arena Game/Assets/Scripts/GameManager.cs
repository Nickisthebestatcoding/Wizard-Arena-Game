using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    public Text coinText; // Reference to the UI text to show coin count
    private int coinCount = 0;
    
    // Call this method to increase the coin count
    public void AddCoin()
    {
        coinCount++; // Increment the coin count
        UpdatecoinText(); // Update the UI text
    }

    // Update the coin count display in the UI
    private void UpdatecoinText()
    {
        coinText.text = "Coins: " + coinCount; // Update the UI text
    }
}