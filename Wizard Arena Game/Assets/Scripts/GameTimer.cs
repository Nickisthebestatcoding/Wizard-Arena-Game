using UnityEngine;
using TMPro; // Use UnityEngine.UI if you're using regular Text
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;
    public TextMeshProUGUI timerText;

    private float timer = 0f;
    private bool isRunning = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Replace "TargetScene" with your actual scene name
        if (scene.name == "FinalFinal")
        {
            isRunning = false;
            Debug.Log("Timer stopped in " + scene.name);
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);

        if (timerText != null)
            timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public void StopTimer() => isRunning = false;
    public void ResetTimer()
    {
        timer = 0f;
        isRunning = true;
    }
}

