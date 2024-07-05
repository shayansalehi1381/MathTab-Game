using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public GameObject LineBarrier;
    public float spawnRate = 5;
    private float timer = 0;

    private float lastYPosition = 0; // Track the y position of the last spawned LineBarrier
    public float yOffset = 10; // Distance between each LineBarrier in y-axis

    private void Start()
    {
        Instantiate(LineBarrier, new Vector3(transform.position.x, lastYPosition, 0), transform.rotation);
    }

    private void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnLineBarrier();
            timer = 0;
        }
    }

    void SpawnLineBarrier()
    { 
        // Increment the y position for the next LineBarrier
        lastYPosition += yOffset;

        Instantiate(LineBarrier, new Vector3(transform.position.x, lastYPosition, 0), transform.rotation);
    }
}
