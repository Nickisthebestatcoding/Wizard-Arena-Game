using UnityEngine;
using UnityEngine.UI; // For updating the UI text
using TMPro;

public class WizardCoinManager : MonoBehaviour
{
    public static WizardCoinManager Instance; // Singleton for easy access
    public int totalCoins = 0; // Track the total number of coins
    public TextMeshProUGUI coinText; // Reference to the UI text element displaying coins

    private void Awake()
    {
        // Ensure only one instance of WizardCoinManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + totalCoins.ToString(); // Update the text with the coin count
        }
    }
}
