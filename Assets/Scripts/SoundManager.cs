using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource soundEffectSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect()
    {
        soundEffectSource.Play();
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectSource.volume = volume;
    }
}
