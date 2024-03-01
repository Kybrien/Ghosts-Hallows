using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMPro.TMP_Text TimerText; // Assign in the inspector
    private float timeRemaining = 300; // 5 minutes in seconds

    void Update()
    {
        if (timeRemaining > 0)
        {
            Debug.Log("Time's still going!");
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time's Up!");
            // Additional logic when the timer ends
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.text = minutes + ":" + seconds;
    }
}
