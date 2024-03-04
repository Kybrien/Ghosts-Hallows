using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMPro.TMP_Text TimerText;
    public TMPro.TMP_Text StartingGame;
    public GoalCondition GoalC;

    private float timeRemaining = 300; // 5 minutes in seconds
    private float countdownTime = 3; //countdown
    private bool isCountdownActive = true;

    public void RestartCountdown()
    {
        timeRemaining = 20; // Reset game timer to 5 minutes
        countdownTime = 3; // Reset countdown timer to 3 seconds
        isCountdownActive = true; // Reactivate countdown
        StartingGame.text = ""; // Clear any starting game text
        TimerText.text = ""; // Clear the main timer text
    }

    void Update()
    {
        if (isCountdownActive)
        {
            if (countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
                StartingGame.text = Mathf.CeilToInt(countdownTime).ToString();
            }
            else
            {
                isCountdownActive = false;
                StartingGame.text = "La partie Commence!";
                Invoke("StartGameTimer", 1); 
            }
        }
        else if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time's Up!");
            StartingGame.text = "Time's Up!";
            Invoke("CallTimerEnd", 1); 

        }

    }
    void CallTimerEnd()
    {
        GoalC.CheckWinnerOnTimer();
    }
    void StartGameTimer()
    {
        StartingGame.text = ""; 
        TimerText.text = "";
        // Start the game timer
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        TimerText.text = minutes + ":" + seconds;
    }
}
