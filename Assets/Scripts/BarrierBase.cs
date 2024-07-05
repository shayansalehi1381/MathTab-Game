using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierBase : MonoBehaviour
{
    public Text text;

    public Barrier1 barrier;


    private float Speed = 2.5f;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float resetPositionX;

    
   // public  bool RightDirection = true;

    public void Start()
    {
        barrier.barrierBases.Add(this);
    }

    void Update()
    {
        moving();
        CheckPositionAndSwitchLocation();
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

    void moving()
    {    
        if (barrier.RightDirection)
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
        }
    }

    void CheckPositionAndSwitchLocation()
    {
        if (barrier.RightDirection)
        {
            maxX = 6.5f;
            resetPositionX = -5.3f;
            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(resetPositionX, transform.position.y, transform.position.z);
            }
        }
        else
        {
            maxX = -5.8f;
            resetPositionX = 6f;
            if (transform.position.x < maxX)
            {
                transform.position = new Vector3(resetPositionX, transform.position.y, transform.position.z);
            }
        }
       
    }
}
