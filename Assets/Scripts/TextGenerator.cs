using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour
{
    public BarrierBase barrierBase;

    private static List<int> uniqueNumbers;
    private static int currentIndex = 0;

    private void Start()
    {
        if (uniqueNumbers == null || uniqueNumbers.Count == 0)
        {
            InitializeUniqueNumbers();
        }

        string generatedText = GenerateRandomOperationString();

        if (barrierBase != null)
        {
            barrierBase.SetText(generatedText);
        }
    }

    void InitializeUniqueNumbers()
    {
        uniqueNumbers = new List<int>();

        // Populate the list with numbers 1 to 20
        for (int i = 1; i <= 20; i++)
        {
            uniqueNumbers.Add(i);
        }

        // Shuffle the list
        for (int i = 0; i < uniqueNumbers.Count; i++)
        {
            int temp = uniqueNumbers[i];
            int randomIndex = Random.Range(0, uniqueNumbers.Count);
            uniqueNumbers[i] = uniqueNumbers[randomIndex];
            uniqueNumbers[randomIndex] = temp;
        }
    }

    string GenerateRandomOperationString()
    {
        // Define the operations
        char[] operations = { '*', '/', '+', '-' };

        // Select a random operation
        char randomOperation = operations[Random.Range(0, operations.Length)];

        // Get a unique random number from the list
        int uniqueRandomNumber = uniqueNumbers[currentIndex];
        if (currentIndex > 3)
        {
            currentIndex = 0;
        }
        currentIndex++;

        // Create the final string
        string operationString = randomOperation.ToString() + uniqueRandomNumber.ToString();

        return operationString;
    }
}
