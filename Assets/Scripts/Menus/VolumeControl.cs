using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {
    public AudioMixer mixer;
    public Slider slider;

    void Start() {
        slider.value = PlayerPrefs.GetFloat("VolumeSlider", 1);
        SetVolume(slider.value);
    }

    public void SetVolume(float sliderValue) {
        float volumeAmount = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("MasterVolume", volumeAmount);
        PlayerPrefs.SetFloat("VolumeSlider", sliderValue);
    }
}