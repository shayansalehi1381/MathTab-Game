using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour
{
    public BarrierBase barrierBase;

    private void Start()
    {
        string generatedText = GenerateRandomOperationString();

        if (barrierBase != null)
        {
            barrierBase.SetText(generatedText);
        }
    }

    string GenerateRandomOperationString()
    {
        // Define the range of the numbers
        int min = -10;
        int max = 10;

        // Generate a random number within the range
        int randomNumber = Random.Range(min, max + 1);

        // Define the operations
        string[] operations = { "+", "*", "/" };

        // Generate a random operation
        string operation = operations[Random.Range(0, operations.Length)];

        // Create the operation string based on the number being positive or negative
        string operationString;
        if (randomNumber < 0)
        {
            if (operation == "+")
            {
                operationString = randomNumber.ToString(); // Just the negative number
            }
            else
            {
                operationString = operation + "(" + randomNumber.ToString() + ")"; // Operation with parentheses
            }
        }
        else if (randomNumber > 0)
        {
            operationString = operation + randomNumber.ToString(); // Operation with positive number
        }
        else
        {
            // Handle the case where the number is zero, if necessary
            operationString = operation + "0";
        }

        return operationString;
    }
}
