using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource audioSource;
    double clipDuration;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        clipDuration = (double)audioSource.clip.samples / audioSource.clip.frequency;
        audioSource.PlayScheduled(AudioSettings.dspTime + 2.0f);
        InvokeRepeating("PlayAudio", (float)clipDuration + 2, 5 + (float)clipDuration);
    }

    void PlayAudio()
    {
        audioSource.PlayScheduled(AudioSettings.dspTime + 5.0f);
    }
}


