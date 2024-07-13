using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeGenerator : MonoBehaviour
{

    [SerializeField]
    private int overallMin = -1000;
    [SerializeField]
    private int overallMax = 2000;
    public Text displayText;

    void Start()
    {
        (int, int) range = GenerateRandomRange();
        string rangeText = $"({range.Item1}, {range.Item2})";

        if (displayText != null)
        {
            displayText.text = rangeText;
        }
        else
        {
            Debug.LogWarning("No Text component assigned to display the generated range.");
        }
    }

    public (int, int) GenerateRandomRange()
    {
        int minRangeLength = 50;
        int maxRangeLength = 200;

        // Randomly determine the length of the range
        int rangeLength = Random.Range(minRangeLength, maxRangeLength + 1);

        // Determine if the range should be positive or negative
        bool isNegativeRange = Random.value > 0.5f;

        int first, second;

        if (isNegativeRange)
        {
            // For negative ranges, ensure the range stays within the negative side of the limit
            int maxFirst = -rangeLength; // Since rangeLength is positive, -rangeLength is the minimum for the second value
            first = Random.Range(overallMin, maxFirst);
            second = first + rangeLength;
        }
        else
        {
            // For positive ranges, ensure the range stays within the positive side of the limit
            int minFirst = 0;
            int maxFirst = overallMax - rangeLength;
            first = Random.Range(minFirst, maxFirst + 1);
            second = first + rangeLength;
        }

        return (first, second);
    }
}
