using UnityEngine;
using System.Collections;

// Sound from Zapsplat.com! Just the background sound not the music
public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    public float delay = 20f; // delay in seconds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundWithDelay());
    }

    IEnumerator PlaySoundWithDelay()
    {
        while (true) // Infinite loop
        {
            yield return new WaitForSeconds(delay); // Wait for the specified delay
            audioSource.Play(); // Play the sound
        }
    }
}
