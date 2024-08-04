using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text ScoreText;
    public void Setup(int score)
    { 
            gameObject.SetActive(true);
            ScoreText.text = "Score : " + score.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
