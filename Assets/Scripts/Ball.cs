using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpSpeed = 15;
    private bool ballAlive = true;
    private Camera mainCamera;
    private BarrierBase barrierBasee;
    private Bomb bomb;
    [SerializeField]
    private AudioSource audioSource; // Reference to the AudioSource

    public void Start()
    {
        rb.isKinematic = true;
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    public void Update()
    {
        if (ballAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }

            if (!IsBallInCameraView())
            {
                Die();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    void Jump()
    {
        rb.isKinematic = false;
        rb.velocity = Vector2.up * jumpSpeed;
        audioSource.Play();
    }

    private bool IsBallInCameraView()
    {
        Vector3 ballPosition = mainCamera.WorldToViewportPoint(transform.position);
        return ballPosition.x >= 0 && ballPosition.x <= 1 && ballPosition.y >= 0 && ballPosition.y <= 1;
    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        ballAlive = false;
        gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "BottomBoundary")
        {
            Die();
        }
        else if (other.gameObject.CompareTag("Barrier"))
        {
            barrierBasee = other.GetComponent<BarrierBase>();
            barrierBasee.barrier.ballCollided++;
            if (barrierBasee != null)
            {
                if (barrierBasee.barrier.ballCollided <= 1)
                {
                    string barrierText = barrierBasee.text.text;
                    mathOperation(barrierText);
                }
            }
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            Die();
        }
    }

    public bool IsBallAlive()
    {
        return ballAlive;
    }

    public void mathOperation(string barrierText)
    {
        char operation = barrierText[0];
        string numberPart = barrierText.Substring(1);

        if (numberPart.StartsWith("(") && numberPart.EndsWith(")"))
        {
            numberPart = numberPart.Substring(1, numberPart.Length - 2);
        }

        int number;
        if (int.TryParse(numberPart, out number))
        {
            switch (operation)
            {
                case '*':
                    Score.score *= number;
                    barrierBasee.barrier.clearAllText();
                    break;
                case '/':
                    if (number != 0)
                    {
                        Score.score /= number;
                        barrierBasee.barrier.clearAllText();
                    }
                    break;
                case '+':
                    Score.score += number;
                    barrierBasee.barrier.clearAllText();
                    break;
                case '-':
                    Score.score -= number;
                    barrierBasee.barrier.clearAllText();
                    break;
                default:
                    Debug.Log("Unknown operation: " + operation);
                    break;
            }
        }
        else
        {
            Debug.Log("Invalid number format: " + numberPart);
        }
    }
}
