using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;

    public Text text;

    void Start()
    {
        score = 1;
    }

    void Update()
    {
        text.text = score.ToString();
    }
}
