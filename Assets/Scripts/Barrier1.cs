using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier1 : MonoBehaviour
{
    public int ballCollided = 0;

    public List<BarrierBase> barrierBases = new List<BarrierBase>();

    public bool RightDirection;

        void Start()
    {
        // Set RightDirection randomly
        RightDirection = Random.value > 0.5f;
    }


    public void clearAllText()
    {
        for (int i = 0; i < barrierBases.Count; i++)
        {
            BarrierBase barrierBase = barrierBases[i];
            barrierBase.text.text = null;
        }
    }
}
