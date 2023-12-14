using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeAudioManager : MonoBehaviour
{
    private AudioSource snakeAudioSource;
   

    void Start()
    {
        // Get the AudioSource component
        snakeAudioSource = GetComponent<AudioSource>();
       

        // Optional: You can set other audio source properties here, such as volume, pitch, etc.

        // Start the Coroutine to play the sound continuously
        StartCoroutine(PlaySnakeSoundCoroutine());
    }
    IEnumerator PlaySnakeSoundCoroutine()
    {
        while (true)
        {
            // Play the snake sound
            snakeAudioSource.Play();

            // Wait for the sound to finish playing before playing it again
            yield return new WaitForSeconds(snakeAudioSource.clip.length);
        }
    }
}
