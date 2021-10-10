using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour {
    public Text endMenuText;
    public Button restartButton;
    public Button quitToMainMenuButton;

    private void Start() {
        gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        quitToMainMenuButton.onClick.AddListener(QuitToMainMenu);
    }

    public void ToggleEndMenu(bool set) {
        gameObject.SetActive(set);
        if (!set) return;
        endMenuText.text = InsultsRecord.IsWin() ? "Enhorabuena!\nHas ganado el juego!" : "Has perdido.\nPrueba a jugar de nuevo.";
    }

    private void RestartGame() {
        ToggleEndMenu(false);
        InsultsRecord.ResetStats();
        SceneManager.LoadScene("Game");
        Debug.Log("Reiniciando partida...");
    }

    private void QuitToMainMenu() {
        ToggleEndMenu(false);
        InsultsRecord.ResetStats();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Accediendo al menú principal...");
    }
}
