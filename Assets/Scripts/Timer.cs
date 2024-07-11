using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timeRemaining = 5f;
    private bool timerRunning = true;
    public Ball ballScript; // Reference to the Ball script

    [SerializeField]
    private Text RangeText;

    [SerializeField]
    private RangeGenerator rangeGenerator; // Ensure this is assigned in the Inspector

    void Start()
    {
        DisplayTime(timeRemaining);
        ResetRange();
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                DisplayTime(timeRemaining);
                CheckScoreAndDie();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float fraction = (timeToDisplay % 1) * 100;
        timerText.text = string.Format("{0:00}.{1:00}", seconds, fraction);
    }

    void CheckScoreAndDie()
    {
        int score = Score.score; // Assuming Score is a static class
        (int minRange, int maxRange) = ParseRangeText();
        Debug.Log("score: " + score);
        Debug.Log("range: " + minRange + " - " + maxRange);

        if (score < minRange || score > maxRange)
        {
           ballScript.Die();
         
        }
        else
        {
            ResetTimerAndRange();
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    void ResetTimerAndRange()
    {
        timeRemaining = 5f;
        timerRunning = true;
        ResetRange();
    }

    void ResetRange()
    {
        // Ensure rangeGenerator is assigned in the Inspector
        if (rangeGenerator != null)
        {
            (int minRange, int maxRange) = rangeGenerator.GenerateRandomRange();
            string rangeText = $"({minRange}, {maxRange})";

            if (RangeText != null)
            {
                RangeText.text = rangeText;
            }
            else
            {
                Debug.LogWarning("No Text component assigned to display the generated range.");
            }
        }
        else
        {
            Debug.LogError("RangeGenerator is not assigned.");
        }
    }

    (int, int) ParseRangeText()
    {
        string rangeText = RangeText.text.Trim('(', ')');
        string[] parts = rangeText.Split(',');

        int minRange = int.Parse(parts[0]);
        int maxRange = int.Parse(parts[1]);

        return (minRange, maxRange);
    }
}
