using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierBase : MonoBehaviour
{
    public Text text;

    public Barrier1 barrier;

    public void Start()
    {
        barrier.barrierBases.Add(this);
    }

    public void SetText(string newText)
    {
            text.text = newText;   
    }
}
