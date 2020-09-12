using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Metodo che da' inizio alla partita (caricando la relativa scena)
    public void NuovaPartita()
    {
        SceneManager.LoadScene(1);
    }

    // Metodo che consente di uscire dal gioco
    public void Esci()
    {
        Application.Quit();
    }
}
