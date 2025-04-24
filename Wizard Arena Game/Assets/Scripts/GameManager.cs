using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance;
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
    void Awake()
    {
        // Ensure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Makes this object persist between scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicates
        }
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}