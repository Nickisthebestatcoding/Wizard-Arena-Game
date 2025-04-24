using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour


{
    public static GameManager Instance;

    public int coinText = 0;

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

    public void AddCoin()
    {
        coinText++;
        Debug.Log("Total Coins: " + coinText);
    }
}