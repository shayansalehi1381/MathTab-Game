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




    public void Start()
    {
        rb.isKinematic = true;
        mainCamera = Camera.main;
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

            if (Score.score <= 0)
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
        gameObject.SetActive(false); // Hide the ball when it dies
      
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
            barrierBasee.barrier.ballCollided ++;
            if (barrierBasee != null)
            {
                if (barrierBasee.barrier.ballCollided <= 1)
                {
                    Debug.Log("Colideddddddddddd");
                    string barrierText = barrierBasee.text.text;
                    mathOperation(barrierText);
                }
            }
        }
    }

    public bool IsBallAlive()
    {
        return ballAlive;
    }

    public void mathOperation(string barrierText)
    {
        // Parse the operation and number
        char operation = barrierText[0];
        int number;
        if (int.TryParse(barrierText.Substring(1), out number))
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
    }
    

    
}
