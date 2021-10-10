using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void NewGame() {
        SceneManager.LoadScene("Game");
        Debug.Log("Iniciando partida...");
    }

    public void QuitToDesktop() {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
