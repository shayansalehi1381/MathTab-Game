using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ballTarget;
    public float yOffset = 2f; // Y gap between ball and camera
    public float smoothSpeed = 0.125f;

    private Vector3 desiredPosition;

    void Start()
    {
        ballTarget = GameObject.FindGameObjectWithTag("Ball").transform;
        transform.position = new Vector3(ballTarget.position.x, ballTarget.position.y + yOffset, -10f);
    }

    void FixedUpdate()
    {

        if (ballTarget.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            desiredPosition = new Vector3(ballTarget.position.x, ballTarget.position.y + yOffset, -10f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
            
    }
}
