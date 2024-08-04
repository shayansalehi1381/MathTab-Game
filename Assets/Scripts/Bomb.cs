using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private AudioClip fuseSound; // Reference to the fuse sound
    [SerializeField]
    private AudioClip explosionSound; // Reference to the explosion sound
    [SerializeField]
    private float lifeTime = 5f; // Lifetime of the bomb
    [SerializeField]
    private float rotationSpeed = 50f;

    private AudioSource audioSource;
    private bool hasExploded = false;

    private void Start()
    {
        // Ensure the bomb has an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the audio source clip to the fuse sound and enable looping
        audioSource.clip = fuseSound;
        audioSource.loop = true;

        // Play the fuse sound
        audioSource.Play();

        // Start the coroutine to destroy the bomb after its lifetime
        StartCoroutine(DestroyAfterDelay());
    }

    private void Update()
    {
        // Rotate the bomb around its Z-axis continuously
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifeTime);

        if (!hasExploded)
        {
            // Destroy the bomb game object if it hasn't exploded
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Instantiate the explosion prefab at the bomb's position and with the bomb's rotation
            Instantiate(Explosion, transform.position, transform.rotation);

            // Stop the fuse sound
            audioSource.Stop();

            // Play the explosion sound
            audioSource.PlayOneShot(explosionSound);

            // Mark the bomb as exploded
            hasExploded = true;

            // Destroy the bomb game object after the explosion sound plays
            Destroy(gameObject);
        }
    }
}
