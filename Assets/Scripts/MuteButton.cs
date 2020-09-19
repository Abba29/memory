using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    //Variabile che serve per capire se il giocatore vuole o meno la musica
    public bool muted = false; 
 
    // Start is called before the first frame update
    void Start()
    {
        muted = false; 
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<AudioManager>().Unmute("theme");
    }



}
