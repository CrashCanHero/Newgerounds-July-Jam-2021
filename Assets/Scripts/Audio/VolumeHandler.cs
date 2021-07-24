using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeHandler : MonoBehaviour {
    public AudioMixer mixer;
    public Slider MasterSlider, SFXSlider, MusicSlider;

    private void Start() {
        if(PlayerPrefs.HasKey("MasterVol")) {
            float value = PlayerPrefs.GetFloat("MasterVol");
            MasterSlider.value = value;
            mixer.SetFloat("Master", GetAttenuatedValue(value));
        }

        if(PlayerPrefs.HasKey("MusicVol")) {
            float value = PlayerPrefs.GetFloat("MusicVol");
            MusicSlider.value = value;
            mixer.SetFloat("Music", GetAttenuatedValue(value));
        }

        if(PlayerPrefs.HasKey("SFXVol")) {
            float value = PlayerPrefs.GetFloat("SFXVol");
            SFXSlider.value = value;
            mixer.SetFloat("SFX", GetAttenuatedValue(value));
        }
    }

    public void MasterChanged(float value) {
        mixer.SetFloat("Master", GetAttenuatedValue(value));
        PlayerPrefs.SetFloat("MasterVol", value);
    }

    public void MusicChanged(float value) {
        mixer.SetFloat("Music", GetAttenuatedValue(value));
        PlayerPrefs.SetFloat("MusicVol", value);
    }

    public void SFXChanged(float value) {
        mixer.SetFloat("SFX", GetAttenuatedValue(value));
        PlayerPrefs.SetFloat("SFXVol", value);
    }

    public float GetAttenuatedValue(float input) {
        return Mathf.Log10(input) * 20f;
    }
}