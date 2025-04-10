
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;  // UI Text element to display the timer
    private float timeElapsed = 0f;  // Time that has passed
    private bool timerRunning = false; // Whether the timer is running// Start is called before the first frame update
    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer Text not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;  // Increment timeElapsed by the time passed since the last frame
            DisplayTime(timeElapsed);
        }
    }

    // Starts the timer
    public void StartTimer()
    {
        timerRunning = true;
    }

    // Stops the timer
    public void StopTimer()
    {
        timerRunning = false;
    }

    // Resets the timer
    public void ResetTimer()
    {
        timerRunning = false;
        timeElapsed = 0f;
        DisplayTime(timeElapsed);
    }

    // Display the time on the UI Text element
    private void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
