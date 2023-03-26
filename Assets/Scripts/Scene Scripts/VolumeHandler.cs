using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    public Slider masterVolume;
    public Slider soundsVolume;
    public float currentMasterVolume;
    public float currentSoundsVolume;
    public GameObject audioManager;
    // Start is called before the first frame update
    void Start()
    {
        masterVolume.value = 1;
        soundsVolume.value = 1;
        DontDestroyOnLoad(audioManager);
    }

    // Update is called once per frame
    void Update()
    {
        currentMasterVolume = masterVolume.value;
        currentSoundsVolume = soundsVolume.value;
        audioManager.GetComponent<AudioManager>().soundsVolume = currentSoundsVolume;
        audioManager.GetComponent<AudioManager>().masterVolume = currentMasterVolume;
    }
}
