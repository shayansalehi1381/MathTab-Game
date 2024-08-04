using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioSource secondAS;

    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip dingSound;

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "JumpSound":
                audioSource.clip = jumpSound;
                audioSource.volume = 0.2f; // Set volume for jump sound
                audioSource.Play();
                break;
            case "DeathSound":
                audioSource.clip = deathSound;
                audioSource.volume = 1f; // Set volume for death sound
                audioSource.Play();
                break;
            case "DingSound":
                secondAS.clip = dingSound;
                secondAS.Play();
                break;
            default:
                Debug.LogWarning("Sound name not recognized: " + soundName);
                break;
        }
    }
}
