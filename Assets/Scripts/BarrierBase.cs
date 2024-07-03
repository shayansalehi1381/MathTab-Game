using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierBase : MonoBehaviour
{
    public Text text;

    public Barrier1 barrier;

    public float Speed = 2.5f;
    public float maxX = 5.6f;

    [SerializeField]
    private float resetPositionX = -4f;

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
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    void CheckPositionAndSwitchLocation()
    {
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(resetPositionX, transform.position.y, transform.position.z);
        }
    }
}
