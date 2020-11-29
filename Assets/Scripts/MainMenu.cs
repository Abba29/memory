using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text gamesPlayed, easyGamesWon, hardGamesWon, easyBestTime, hardBestTime;

    private void NewEasyGame()
    {
        PlayerPrefs.SetString("LastGameModeSelected", "Easy");
        SceneManager.LoadScene(1);
    }

    private void NewHardGame()
    {
        PlayerPrefs.SetString("LastGameModeSelected", "Hard");
        SceneManager.LoadScene(1);
    }

    // Print the values shown in the 'Statistiche' game screen
    private void PrintStats()
    {
        gamesPlayed.text = "Partite Giocate: " + PlayerPrefs.GetInt("GamesPlayed");
        easyGamesWon.text = "Partite Vinte (Facile): " + PlayerPrefs.GetInt("EasyGamesWon");
        hardGamesWon.text = "Partite Vinte (Difficile): " + PlayerPrefs.GetInt("HardGamesWon");
        
        easyBestTime.text = "Miglior Tempo (Facile): " + PlayerPrefs.GetFloat("EasyBestTime").ToString("0.0").Replace(",", ".") + " s";
        hardBestTime.text = "Miglior Tempo (Difficile): " + PlayerPrefs.GetFloat("HardBestTime").ToString("0.0").Replace(",", ".") + " s";
    }

    // Reset the values shown in the 'Statistiche' game screen
    private void ResetStats()
    {
        PlayerPrefs.SetInt("GamesPlayed", 0);
        PlayerPrefs.SetInt("EasyGamesWon", 0);
        PlayerPrefs.SetInt("HardGamesWon", 0);
        PlayerPrefs.SetFloat("EasyBestTime", 1000f);
        PlayerPrefs.SetFloat("HardBestTime", 1000f);

        PrintStats();
    }

    public void QuitGame()
    {
        Application.OpenURL("about:blank");
    }
}
