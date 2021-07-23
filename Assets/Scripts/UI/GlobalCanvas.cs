using System.Collections;
using System.Collections.Generic;
using Toolnity;
using UnityEngine;
using TMPro;

public class GlobalCanvas : MonoBehaviour {
    public static GlobalCanvas Instance;

    public Fader FadeController;
    public TMP_Text GameOverText;
    public GameObject RestartButton, QuitButton;

    string gameOverString;
    int gameOverIndex;

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
    }
}