using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject bombPrefab; // Reference to the bomb prefab
    [SerializeField]
    private float spawnInterval = 4f; // Interval between spawns
    [SerializeField]
    private float xSpeed = 2f; // Fixed x speed
    [SerializeField]
    private Vector2 ySpeedRange = new Vector2(2f, 8f); // Range of y speeds
    [SerializeField]
    private float moveSpeed = 2f; // Speed of vertical movement
    [SerializeField]
    private float moveDistance = 2f; // Distance to move up and down

    private Vector3 startPosition;
    private Coroutine spawnCoroutine;
    [SerializeField]
    private int ID;

    private void Start()
    {
        startPosition = transform.position;
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnBombs());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnBombs()
    {
        while (true)
        {
            SpawnBomb();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        // Fixed x speed
        float ySpeed = Random.Range(ySpeedRange.x, ySpeedRange.y);

        // Apply the velocity
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
}
