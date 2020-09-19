using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerButton : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MuteButton>().muted = true; 
    }

    // Update is called once per frame
    void Update()
    {

        FindObjectOfType<AudioManager>().Mute("theme");
    
    }


}
