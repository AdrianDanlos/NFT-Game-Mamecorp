using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.playOnAwake = false;

            if (s.clip.name.Contains("main_theme") || s.clip.name.Contains("waiting"))
            {
                s.source.loop = true;
            }
        }
    }

    private void Start()
    {
        if (!IsSourcePlaying("Theme"))
            Play("Theme");
    }

    private void Update()
    {
        // TODO
        // on main menu allow to change music & sound settings
        if (SceneManager.GetActiveScene().name.Contains("MainMenu"))
            Debug.Log("true");
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

    // needed to add sounds to instantiated objects
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
}
