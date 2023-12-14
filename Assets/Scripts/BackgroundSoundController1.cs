using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController1 : MonoBehaviour
{

    private AudioSource audioSource;
    public float volumeChangeSpeed = 0.1f; // Adjust the speed of volume change
    private bool increasing = false;
    private bool decreasing = false;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Play the background sound on start
        PlayBackgroundSound();
    }

    void Update()
    {
        // Example: Press and hold the 'I' key to increase the volume
        if (Input.GetKeyDown(KeyCode.I))
        {
            increasing = true;
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            increasing = false;
        }

        // Example: Press and hold the 'D' key to decrease the volume
        if (Input.GetKeyDown(KeyCode.C))
        {
            decreasing = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            decreasing = false;
        }

        // Adjust the volume based on the keys being held
        if (increasing)
        {
            IncreaseVolume();
        }
        else if (decreasing)
        {
            DecreaseVolume();
        }
    }

    void PlayBackgroundSound()
    {
        // Check if the audio source and audio clip are set
        if (audioSource != null && audioSource.clip != null)
        {
            // Play the background sound
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip not set!");
        }
    }

    void IncreaseVolume()
    {
        audioSource.volume = Mathf.Min(1f, audioSource.volume + volumeChangeSpeed * Time.deltaTime);
    }

    void DecreaseVolume()
    {
        audioSource.volume = Mathf.Max(0f, audioSource.volume - volumeChangeSpeed * Time.deltaTime);
    }
}
