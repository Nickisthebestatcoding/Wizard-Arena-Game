using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer 
{
    private TextMeshProUGUI timeText;
    float elapsedTime;
    public Timer(TextMeshProUGUI textfield)
    {
        timeText = textfield;
        elapsedTime = 0;
        textfield.text = "Time: 0";
    }


    public void Update()
    {
        elapsedTime += Time.deltaTime;
        timeText.text = "Time: " + (int)elapsedTime;
    }
}
