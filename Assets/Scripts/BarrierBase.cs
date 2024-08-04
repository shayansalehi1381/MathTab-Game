using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierBase : MonoBehaviour
{
    public Text UItext;
    public string mathString;
    public Barrier1 barrier;
    public int answer;

    [SerializeField]
    public float Speed;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float resetPositionX;

    public void Start()
    {
        barrier.barrierBases.Add(this);
        Speed = BarrierSpawner.speedForBarrierBase;
    }

    void Update()
    {
        moving();
        CheckPositionAndSwitchLocation();
    }

    public void SetUIText(string newText)
    {
        UItext.text = newText;
        
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
