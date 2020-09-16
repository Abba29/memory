using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    // Use Awake to initialize variables or states before the application starts
    private void Awake()
    {
        instance = this;
    }

    private TimeSpan timePlaying;
    
    public float timeElapsed = 0f;
    public TextMesh textBox;

    bool timerGoing = false;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "Tempo: 00.00 s";
    }

    // Update is called once per frame
    void Update()
    {
        if(timerGoing)
        {
            timeElapsed += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(timeElapsed);
            string timePlayingStr = "Tempo: " + timePlaying.ToString("ss'.'f") + " s";
            textBox.text = timePlayingStr;
        }
    }

    public void BeginTimer()
    {
        timerGoing = true;
    }

    public void EndTimer()
    {
        timerGoing = false;
    }
}
