using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    private Animator animator;
    private int currentAnimation;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {

        if (currentAnimation == Animator.StringToHash(animationName))
            return;


        animator.Play(animationName);
        currentAnimation = Animator.StringToHash(animationName);
    }
}
