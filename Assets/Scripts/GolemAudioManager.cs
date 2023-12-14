using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource golemAudioSource;


    void Start()
    {
        // Get the AudioSource component
        golemAudioSource = GetComponent<AudioSource>();

    
    }
    
     public void PlayGolemLaughSound()
    {

        // Play theluffy  sound
        golemAudioSource.Play();

           
    }
}
