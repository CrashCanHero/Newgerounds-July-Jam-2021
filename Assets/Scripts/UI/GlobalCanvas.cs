using System.Collections;
using System.Collections.Generic;
using Toolnity;
using UnityEngine;
using TMPro;

public class GlobalCanvas : MonoBehaviour {
    public static GlobalCanvas Instance;

    public Fader FadeController;
    public GameObject GameOverScreen;
    public TMP_Text GameOverText;
    public GameObject RestartButton, QuitButton;
    public GameObject WinScreen;
    public TMP_Text WinText;

    string gameOverString;
    int gameOverIndex;

    string winString;
    int winIndex;

    private void Awake() {
        if(!Instance) {
            Instance = this;
        }
    }

    private void Update() {
        if(GameOverText.gameObject.activeSelf && gameOverIndex < "Ship Status: Offline".Length) {
            GameOverText.text += "Ship Status: Offline"[gameOverIndex];
            gameOverIndex++;
        }

        if(WinText.gameObject.activeSelf && winIndex < "You head home, taking whatever you could grab with you".Length) {
            WinText.text += "You head home, taking whatever you could grab with you"[winIndex];
            winIndex++;
        }
    }
}