using io.newgrounds.objects;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Volume_Controller : MonoBehaviour
{
    public AudioSource[] music;
    public AudioSource[] sfx;
    public Slider[] Sliders;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !PlayerPrefs.HasKey("Master"))
        {
            PlayerPrefs.SetFloat("Master", Sliders[2].GetComponent<Slider>().value);
            PlayerPrefs.SetFloat("Music", Sliders[0].GetComponent<Slider>().value);
            PlayerPrefs.SetFloat("SFX", Sliders[1].GetComponent<Slider>().value);
            PlayerPrefs.Save();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            music[0].volume = (1f * PlayerPrefs.GetFloat("Music")) * PlayerPrefs.GetFloat("Master");
            Sliders[0].GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music");
            Sliders[1].GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX");
            Sliders[2].GetComponent<Slider>().value = PlayerPrefs.GetFloat("Master");
        }
        else 
        {
            music[0].volume = (1f * PlayerPrefs.GetFloat("Music")) * PlayerPrefs.GetFloat("Master");
            music[1].volume = (1f * PlayerPrefs.GetFloat("Music")) * PlayerPrefs.GetFloat("Master");
            sfx[0].volume = (1f * PlayerPrefs.GetFloat("SFX")) * PlayerPrefs.GetFloat("Master");
            sfx[1].volume = (1f * PlayerPrefs.GetFloat("SFX")) * PlayerPrefs.GetFloat("Master");
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (PlayerPrefs.GetFloat("Master") != Sliders[2].value)
            {
                music[0].volume = (1f * Sliders[0].value) * Sliders[2].value;
                sfx[0].volume = (1f * Sliders[1].value) * Sliders[2].value;
                PlayerPrefs.SetFloat("Master", Sliders[2].value);
                PlayerPrefs.Save();
            }
            if (PlayerPrefs.GetFloat("SFX") != Sliders[1].value)
            {
                sfx[0].volume = (1f * Sliders[1].value) * Sliders[2].value;
                PlayerPrefs.SetFloat("SFX", Sliders[1].value);
                PlayerPrefs.Save();
            }
            if (PlayerPrefs.GetFloat("Music") != Sliders[0].value)
            {
                music[0].volume = (1f * Sliders[0].value) * Sliders[2].value;
                PlayerPrefs.SetFloat("Music", Sliders[0].value);
                PlayerPrefs.Save();
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            music[0].volume = (1f * PlayerPrefs.GetFloat("Music")) * PlayerPrefs.GetFloat("Master");
            music[1].volume = (1f * PlayerPrefs.GetFloat("Music")) * PlayerPrefs.GetFloat("Master");
            sfx[0].volume = (1f * PlayerPrefs.GetFloat("SFX")) * PlayerPrefs.GetFloat("Master");
            sfx[1].volume = (1f * PlayerPrefs.GetFloat("SFX")) * PlayerPrefs.GetFloat("Master");
            Object.Destroy(this);
        }
    }
}
