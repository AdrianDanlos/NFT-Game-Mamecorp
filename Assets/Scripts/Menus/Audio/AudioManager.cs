using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // TODO
    // - one theme on main menu + shop, inventory...
    // - one theme for combat
    // - one theme for loading
    // - sound effects on combat
    // - sound effects on chest opening, cards...

    // can use it anywhere
    // -> FindObjectOfType<AudioManager>().Play("Theme");

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

            // loop main themes, rest don't
            if (s.clip.name.Contains("main_theme") || s.clip.name.Contains("waiting"))
            {
                s.source.loop = true;
            } else
            {
                s.source.outputAudioMixerGroup = s.audioMixerGroup;
            }
        }
    }

    private void Start()
    {
        if (!IsSourcePlaying("Waiting"))
            Play("Waiting");
    }

    private void Update()
    {
        // TODO
        // on main menu allow to change music & sound settings
        //if (SceneManager.GetActiveScene().name.Contains("MainMenu"))
    }

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
