using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private float previousVolume ;
    private double volume = 0.50;
    private bool muted = false; 

    
    
    public void Awake()
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
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 
        }
    }

    public void Start()
    {
        Play("theme");

    }

    public void Update()
    {
        if (muted)
        {
            Mute("theme");
        }
        else
        {
            Unmute("theme"); 
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        previousVolume= s.source.volume;
        s.source.volume = 0f;
    }

    public void Unmute(string name)

    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
       
        s.source.volume = (float)volume;
    }

    public void setmuded (bool x)
    {
        bool y = x;
        this.muted = y; 
    }

    public bool getMuted()
    {
        bool x = this.muted;
        return x; 
    }

}
