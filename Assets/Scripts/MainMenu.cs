using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text gamesPlayed, easyGamesWon, hardGamesWon, easyBestTime, hardBestTime;
    
    // Metodo che da' inizio alla partita (caricando la relativa scena)
    public void NuovaPartitaFacile()
    {
        PlayerPrefs.SetString("LastGameModeSelected", "Easy");
        SceneManager.LoadScene(1);
    }

    public void NuovaPartitaDifficile()
    {
        PlayerPrefs.SetString("LastGameModeSelected", "Hard");
        SceneManager.LoadScene(1);
    }

    public void AzzeraStatistiche()
    {
        PlayerPrefs.SetInt("GamesPlayed", 0);
        PlayerPrefs.SetInt("EasyGamesWon", 0);
        PlayerPrefs.SetInt("HardGamesWon", 0);
        PlayerPrefs.SetFloat("EasyBestTime", 1000f);
        PlayerPrefs.SetFloat("HardBestTime", 1000f);

        StampaStatistiche();
    }

    public void StampaStatistiche()
    {
        gamesPlayed.text = "Partite Giocate: " + PlayerPrefs.GetInt("GamesPlayed");
        easyGamesWon.text = "Partite Vinte (Facile): " + PlayerPrefs.GetInt("EasyGamesWon");
        hardGamesWon.text = "Partite Vinte (Difficile): " + PlayerPrefs.GetInt("HardGamesWon");
        
        // da cambiare per accettare float
        easyBestTime.text = "Miglior Tempo (Facile): " + PlayerPrefs.GetFloat("EasyBestTime").ToString("0.0").Replace(",", ".") + " s";
        hardBestTime.text = "Miglior Tempo (Difficile): " + PlayerPrefs.GetFloat("HardBestTime").ToString("0.0").Replace(",", ".") + " s";
    }
    
    // Metodo che consente di uscire dal gioco
    public void Esci()
    {
        Application.Quit();
    }
}
