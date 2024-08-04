using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    [SerializeField]
    Text PointText;
    [SerializeField]
    private Ball ball;

    public Text HighScore;
    public int HighScoreNumber;

    private void Start()
    {
        HighScore.text = "HighScore: "+ PlayerPrefs.GetInt("HighScore",0).ToString();
        HighScoreNumber = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Update()
    {
        setPointText();
    }

    public void setPointText()
    {
        PointText.text = "Score: "+ball.points;
        if (ball.points > HighScoreNumber)
        {
            PlayerPrefs.SetInt("HighScore", ball.points);
            HighScore.text = "HighScore: " + ball.points.ToString();
            HighScoreNumber = ball.points;
        }
        
    }
}
