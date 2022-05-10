using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    // UI
    GameObject sounds;
    GameObject music;
    Slider sliderSounds;
    Slider sliderMusic;
    Image soundOn;
    Image soundOff;
    Image musicOn;
    Image musicOff;

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        // UI
        sounds = GameObject.Find("ListSounds");
        music = GameObject.Find("ListMusic");
        sliderSounds = GameObject.Find("SliderSounds").GetComponent<Slider>();
        sliderMusic = GameObject.Find("SliderMusic").GetComponent<Slider>();
        soundOn = GameObject.Find("SoundOn").GetComponent<Image>();
        soundOff = GameObject.Find("SoundOff").GetComponent<Image>();
        musicOn = GameObject.Find("MusicOn").GetComponent<Image>();
        musicOff = GameObject.Find("MusicOff").GetComponent<Image>();

        // onValueChange
        sliderSounds.onValueChanged.AddListener(SoundListener);
        sliderMusic.onValueChanged.AddListener(MusicListener);
    }

    public void SoundListener(float value)
    {
        if (value == 0)
        {
            soundOn.enabled = false;
            soundOff.enabled = true;
        }
        else
        {
            soundOn.enabled = true;
            soundOff.enabled = false;
        }
    }

    // Slider scale is 0-10 and audio is 0-1
    public void MusicListener(float value)
    {
        if (value == 0)
        {
            FindObjectOfType<AudioManager>().ChangeVolume("Waiting", value / 10);
            musicOn.enabled = false;
            musicOff.enabled = true;
        }
        else
        {
            FindObjectOfType<AudioManager>().ChangeVolume("Waiting", value / 10);
            musicOn.enabled = true;
            musicOff.enabled = false;
        }
    }
}
