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
        // TODO divide value per 10 to set volume in audiomanager
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

    public void MusicListener(float value)
    {
        if (value == 0)
        {
            musicOn.enabled = false;
            musicOff.enabled = true;
        }
        else
        {
            musicOn.enabled = true;
            musicOff.enabled = false;
        }
    }
}
