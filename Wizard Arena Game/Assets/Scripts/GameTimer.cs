using UnityEngine;
using TMPro; // Use UnityEngine.UI if you're using regular Text

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Replace with `public Text timerText;` if using UI.Text
    private float timer = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000f);

        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
        isRunning = true;
    }
}
