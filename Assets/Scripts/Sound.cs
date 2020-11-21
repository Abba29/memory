using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;

    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;

    [Range(0f, 1f)]
    private float volume;

    [Range(.1f,3f)]
    private float pitch;

    private bool loop;

    /*
     * GETTERS
     */

    public AudioClip Clip
    {
        get { return clip; }
    }
    
    public string Name 
    { 
        get { return name; } 
    }

    public float Volume
    {
        get { return volume; }
    }

    public float Pitch
    {
        get { return pitch; }
    }

    public bool Loop
    {
        get { return loop; }
    }

}
