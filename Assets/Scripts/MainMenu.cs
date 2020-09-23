using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text gamesPlayed, easyGamesWon, hardGamesWon, easyBestTime, hardBestTime;

    public void NewEasyGame()
    {
        PlayerPrefs.SetString("LastGameModeSelected", "Easy");
        SceneManager.LoadScene(1);
    }

    public void NewHardGame()
    {
        PlayerPrefs.SetString("LastGameModeSelected", "Hard");
        SceneManager.LoadScene(1);
    }

    // Reset the values shown in the 'Statistiche' game screen
    public void ResetStats()
    {
        PlayerPrefs.SetInt("GamesPlayed", 0);
        PlayerPrefs.SetInt("EasyGamesWon", 0);
        PlayerPrefs.SetInt("HardGamesWon", 0);
        PlayerPrefs.SetFloat("EasyBestTime", 1000f);
        PlayerPrefs.SetFloat("HardBestTime", 1000f);

        //Debug.Log("GamesPlayed " + PlayerPrefs.GetInt("GamesPlayed"));
        //Debug.Log("EasyGamesWon " + PlayerPrefs.GetInt("EasyGamesWon"));
        //Debug.Log("HardGamesWon " + PlayerPrefs.GetInt("HardGamesWon"));
        //Debug.Log("EasyBestTime " + PlayerPrefs.GetInt("EasyBestTime"));
        //Debug.Log("HardBestTime " + PlayerPrefs.GetInt("HardBestTime"));

        PrintStats();
    }

    // Print the values shown in the 'Statistiche' game screen
    public void PrintStats()
    {
        gamesPlayed.text = "Partite Giocate: " + PlayerPrefs.GetInt("GamesPlayed");
        easyGamesWon.text = "Partite Vinte (Facile): " + PlayerPrefs.GetInt("EasyGamesWon");
        hardGamesWon.text = "Partite Vinte (Difficile): " + PlayerPrefs.GetInt("HardGamesWon");
        
        easyBestTime.text = "Miglior Tempo (Facile): " + PlayerPrefs.GetFloat("EasyBestTime").ToString("0.0").Replace(",", ".") + " s";
        hardBestTime.text = "Miglior Tempo (Difficile): " + PlayerPrefs.GetFloat("HardBestTime").ToString("0.0").Replace(",", ".") + " s";
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
