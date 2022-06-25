using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    public AudioMixer soundsMixer;
    private string outputMixer = "SoundEffects";

    private void Awake()
    {
        // singleton
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // load mixer
        soundsMixer = Resources.Load<AudioMixer>("SFX/SoundEffects");

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.playOnAwake = false;

            if (s.clip.name.Contains("main_theme") || s.clip.name.Contains("combat_theme"))
            {
                s.source.loop = true;
                continue;
            } 
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    private void Update()
    {
        // TODO
        // on main menu allow to change music & sound settings
        //if (SceneManager.GetActiveScene().name.Contains("MainMenu"))
    }

    //TODO: Accept a SoundType enum instead of a string
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }

    public void StopAll()
    {
        foreach(Sound sound in sounds){
            if (sound == null)
                return;
            sound.source.Stop();
        }
    }

    public void StopAllAndPlay(string name){
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play(name);
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Pause();
    }

    // if needed to add sounds to instantiated objects
    public void PlayClipAtPoint(string name, Vector2 transform)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        AudioSource.PlayClipAtPoint(s.source.clip, transform);
    }

    public bool IsSourcePlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return false;
        return s.source.isPlaying;
    }

    // TODO change volume of all 
    public void ChangeVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.volume = volume;
    }
}
