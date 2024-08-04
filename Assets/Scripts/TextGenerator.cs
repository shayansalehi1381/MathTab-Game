using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        string generatedText = GenerateRandomOperationString();
       

        if (barrierBase != null)
        { 
            barrierBase.SetUIText(generatedText);
            barrierBase.mathString = generatedText;
        }

    }

    string GenerateRandomOperationString()
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

        string operationString = operation + numberString;

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

        return operationString;
    }


}
