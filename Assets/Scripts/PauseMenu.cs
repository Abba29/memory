using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Enable/disable pause menu only when 'ESC' key is pressed and the game is still running
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.gameRunning)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    private void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
