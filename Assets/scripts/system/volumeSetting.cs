using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeSetting : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider BgmSlider;
    public Slider SfxSlider;


    private void Start()
    {
        if (PlayerPrefs.HasKey("bgmVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBgmVolme();
            SetSFXVolme();
            SetmasterVolme();
        }
    }

    public void SetmasterVolme()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MASTERVolume", volume);
    }

    public void SetBgmVolme()
    {
        float volume = BgmSlider.value;
        audioMixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }

    public void SetSFXVolme()
    {
        float volume = SfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }


    public void LoadVolume()
    {
        BgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        SfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        masterSlider.value = PlayerPrefs.GetFloat("MASTERVolume");
        SetBgmVolme();
        SetSFXVolme();
        SetmasterVolme();
    }
}