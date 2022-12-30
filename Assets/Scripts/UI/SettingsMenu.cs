using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider { get; private set; } 
    public Slider sfxSlider { get; private set; }

    SoundManager SM;


    void Start()
    {
        SM = FindObjectOfType<SoundManager>();
        musicSlider = GameObject.FindGameObjectWithTag("Music Slider").GetComponent<Slider>();
        sfxSlider = GameObject.FindGameObjectWithTag("SFX Slider").GetComponent<Slider>();

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
    }

}
