using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpSpeed = 15;
    public bool ballAlive = true;
    private Camera mainCamera;
    private BarrierBase barrierBasee;
    private Barrier1 LineBarrier;
    private Bomb bomb;
    public int points = 0;

    public SoundManager soundManager;
    public SoundManager secSoundManager;
    public GameOver gameOverScreenPrefab;
    [SerializeField]
    private BallAnimation ballAnimation;

    public void Start()
    {
        rb.isKinematic = true;
        mainCamera = Camera.main;
        soundManager = FindObjectOfType<SoundManager>(); // Find the SoundManager in the scene
        secSoundManager = FindObjectOfType<SoundManager>();
    }

    public void Update()
    {
        if (ballAlive)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Jump();
            }

            if (!IsBallInCameraView())
            {
                Die();
            }
        }
    }


    private void ShowGameOverScreen()
    {
        Vector3 position = mainCamera.transform.position;
        position.z += 10;
        position.y += 2.7f;
        position.x += 0.16f;
        GameOver gameOverInstance = Instantiate(gameOverScreenPrefab, position, Quaternion.identity);
        gameOverInstance.Setup(points);
    }

    void Jump()
    {
        rb.isKinematic = false;
        rb.velocity = Vector2.up * jumpSpeed;
        soundManager.PlaySound("JumpSound"); // Play the jump sound
    }

    private bool IsBallInCameraView()
    {
        Vector3 ballPosition = mainCamera.WorldToViewportPoint(transform.position);
        return ballPosition.x >= 0 && ballPosition.x <= 1 && ballPosition.y >= 0 && ballPosition.y <= 1;
    }

    public void Die()
    {
        ballAnimation.PlayAnimation("BallDeath");
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        ballAlive = false;
        soundManager.PlaySound("DeathSound"); // Play the death sound
        StartCoroutine(DelayedActions()); // Start the coroutine
    }

    private IEnumerator DelayedActions()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 1 second
        ShowGameOverScreen();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BottomBoundary")
        {
            Die();
        }
        else if (other.gameObject.CompareTag("Barrier"))
        {
            BarrierBase barrierBase = other.GetComponent<BarrierBase>();
            ApplyBarrierOperation(barrierBase.mathString);
            barrierBase.barrier.clearAllText();
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            Die();
        }
    }

    private void ApplyBarrierOperation(string operationString)
    {
        char operation = operationString[0];
        string numberPart = operationString.Substring(1);

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
                    Score.score *= number;
                    points += 100;
                    break;
                case '÷':
                    if (number != 0)
                    {
                        Score.score /= number;
                        points += 100;
                    }
                    break;
                case '+':
                    Score.score += number;
                    points += 100;
                    break;
                case '-':
                    Score.score -= number;
                    points += 100;
                    break;
                default:
                    break;
            }

            if (Score.score < 1)
            {
                Die();
            }
        }
    }

    public bool IsBallAlive()
    {
        return ballAlive;
    }
}
