using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float soundsVolume;
    public float masterVolume;
    public int index;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake() {
        PlayMusic();
    }
    

    public void PlayMusic() {
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }

    public void PauseOrResumeMusic() {
        if (audioSource.isPlaying) {
            audioSource.Pause();
        } else {
            audioSource.Play();
        }
        
    }

    private void Update() {
        audioSource.volume = masterVolume;
        if (!audioSource.isPlaying) {
            index++;
            if (index >= audioClips.Length) {
                index = 0;
            }
            audioSource.clip = audioClips[index];
        }
    }
    
}
