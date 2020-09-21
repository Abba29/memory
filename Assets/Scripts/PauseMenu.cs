using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;



    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<AudioManager>().Mute("theme");
        // Enable/disable pause menu only when 'ESC' key is pressed and the game is still running
        if (Input.GetKeyDown(KeyCode.Escape) && GameController.gameRunning)
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().Unmute("theme");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        if (FindObjectOfType<AudioManager>().getMuted())
        {
            FindObjectOfType<AudioManager>().Mute("theme");
        }
        else
        {
            FindObjectOfType<AudioManager>().Unmute("theme");
        }
        SceneManager.LoadScene(0);
    }
}
