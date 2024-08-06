using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Barrier1 : MonoBehaviour
{
    public int ballCollided = 0;
    public List<BarrierBase> barrierBases = new List<BarrierBase>();
    public List<int> scores = new List<int>();
    public bool RightDirection;

    [SerializeField]
    private Ball ballScript;
  
    void Start()
    {
        // Set RightDirection randomly
        RightDirection = Random.value > 0.5f;
        ballScript = GetComponent<Ball>();  
    }

    public void clearAllText()
    {
        for (int i = 0; i < barrierBases.Count; i++)
        {
            BarrierBase barrierBase = barrierBases[i];
            barrierBase.UItext.text = null;
            barrierBase.mathString = null;
        }
    }
}
