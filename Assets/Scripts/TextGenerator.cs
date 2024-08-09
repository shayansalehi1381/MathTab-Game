using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TextGenerator : MonoBehaviour
{
    public BarrierBase barrierBase;

    [SerializeField]
    private int firstNumber = -20;
    [SerializeField]
    private int secondNumber = 31;
    [SerializeField]
    private int thirdNumber = -200;
    [SerializeField]
    private int fourthNumber = 200;

    Ball ballScript;
    [SerializeField]
    Barrier1 barrier;

    private void Start()
    {
        ballScript = FindObjectOfType<Ball>();
        int currentScore = Score.score; // Assuming ballScript has a score property

        // Assume each barrier has 4 barrierBases
        for (int i = 0; i < barrier.barrierBases.Count; i++)
        {
            bool isLastBarrierBase = (i == barrier.barrierBases.Count - 1); // Check if it's the last BarrierBase
            string generatedText = GenerateRandomOperationString(currentScore, isLastBarrierBase);

            BarrierBase currentBarrierBase = barrier.barrierBases[i];
            if (currentBarrierBase != null)
            {
                currentBarrierBase.SetUIText(generatedText);
                currentBarrierBase.mathString = generatedText;
            }

            // Update current score after applying the operation
            currentScore = compareOperation(generatedText, currentScore);
        }
    }


    string GenerateRandomOperationString(int currentBallScore, bool isLastBarrierBase)
    {
        string operationString;
        int tempScore;

        do
        {
            string[] operations = { "+", "-", "×", "÷" };
            string operation = operations[Random.Range(0, operations.Length)];

            int randomNumber;

            if (operation == "×" || operation == "÷")
            {
                randomNumber = Random.Range(firstNumber, secondNumber); // Range for × and ÷
            }
            else
            {
                randomNumber = Random.Range(thirdNumber, fourthNumber); // Range for + and -
            }

            if (operation == "÷" && randomNumber == 0)
            {
                randomNumber = 1; // Avoid division by zero
            }

            string numberString = randomNumber.ToString();

            // Add parentheses for negative numbers in multiplication and division
            if ((operation == "×" || operation == "÷") && randomNumber < 0)
            {
                numberString = $"({randomNumber})";
            }

            operationString = operation + numberString;

            // Handle special cases
            if (operationString.Contains("+-"))
            {
                operationString = operationString.Replace("+-", "-");
            }
            else if (operationString.Contains("-+"))
            {
                operationString = operationString.Replace("-+", "-");
            }
            else if (operationString.Contains("--"))
            {
                operationString = operationString.Replace("--", "+");
            }

            tempScore = compareOperation(operationString, currentBallScore);

            // If this is the last barrierBase and applying this operation makes the score < 1, 
            // make sure to adjust the operation so that it keeps the ball alive
            if (isLastBarrierBase && tempScore < 1)
            {
                // Adjust the operation to keep score above 1
                tempScore = Mathf.Max(1, currentBallScore); // Ensure at least 1
                operationString = "+0"; // Default to a no-op, or adjust as needed
            }
            else
            {
                barrier.tempScoress.Add(tempScore);
            }

        } while (checkTempScores() == false && !isLastBarrierBase); // Repeat the process if checkTempScores returns false and it's not the last base

        return operationString; // Return the final string when checkTempScores is true or it's the last base
    }

    public int compareOperation(string operationString, int currentScore)
    {
        char operation = operationString[0];
        string numberPart = operationString.Substring(1);
        int score = currentScore;
        if (numberPart.StartsWith("(") && numberPart.EndsWith(")"))
        {
            numberPart = numberPart.Substring(1, numberPart.Length - 2);
        }
        int number;
        if (int.TryParse(numberPart, out number))
        {
            switch (operation)
            {
                case '×':
                    score *= number;
                    break;
                case '÷':
                    if (number != 0)
                    {
                        score /= number;
                    }
                    break;
                case '+':
                    score += number;
                    break;
                case '-':
                    score -= number;
                    break;
                default:
                    break;
            }
        }
        return score;
    }


    public bool checkTempScores()
    {
        // Check if any number in the list is negative
        bool hasNegativeOrZero = barrier.tempScoress.All(n => n < 1);

        if (hasNegativeOrZero)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
