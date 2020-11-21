using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    // Awake is used to initialize variables or states before the application starts
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private TextMesh textBox;
   
    private bool timerGoing = false;
    private float timeElapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = "Tempo: 00.0 s";
    }

    // Update is called once per frame
    void Update()
    {
        if (timerGoing)
        {
            timeElapsed += Time.deltaTime;
            string timePlayingStr = "Tempo: " + timeElapsed.ToString("00.0").Replace(",", ".") + " s";
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

    public void CheckBestTime()
    {
        if (PlayerPrefs.GetString("LastGameModeSelected") == "Easy")
        {
            if (timeElapsed < PlayerPrefs.GetFloat("EasyBestTime"))
            {
                PlayerPrefs.SetFloat("EasyBestTime", timeElapsed);
            }
        }
        else
        {
            if (timeElapsed < PlayerPrefs.GetFloat("HardBestTime"))
            {
                PlayerPrefs.SetFloat("HardBestTime", timeElapsed);
            }
        }
    }
}