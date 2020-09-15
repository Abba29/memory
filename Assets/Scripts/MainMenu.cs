using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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

    // Metodo che consente di uscire dal gioco
    public void Esci()
    {
        Application.Quit();
    }
}
