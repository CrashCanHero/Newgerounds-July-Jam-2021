using System.Collections;
using System.Collections.Generic;
using Toolnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GlobalCanvas : MonoBehaviour {
    public static GlobalCanvas Instance;

    public Fader FadeController;
    public GameObject GameOverScreen;
    public TMP_Text GameOverText;
    public Image RestartButton, QuitButton;
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
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        GameManager.Instance.OnLevelComplete += GameComplete;
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

    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
        FadeController.FadeIn();

        LeanTween.value(1f, 0f, 1f).setOnUpdate((float value) => {
            GameOverScreen.GetComponent<CanvasGroup>().alpha = value;
        }).setOnComplete(() => {
            QuitButton.color = RestartButton.color = new Color(1f, 1f, 1f, 0f);
            GameOverText.text = gameOverString = string.Empty;
            gameOverIndex = 0;
            winIndex = 0;
            GameOverText.gameObject.SetActive(false);
            GameOverScreen.SetActive(false);
            GameOverScreen.GetComponent<CanvasGroup>().alpha = 1f;
        });
    }

    public void Restart() {
        foreach(EnemyHealth enemy in FindObjectsOfType<EnemyHealth>()) {
            Destroy(enemy.gameObject);
        }

        foreach(Egg egg in FindObjectsOfType<Egg>()) {
            Destroy(egg.gameObject);
        }

        MusicPlayer.Instance.Play();
        UIHandler.Instance.Score = 0;
        UIHandler.Instance.ScoreText.text = "0";

        LeanTween.value(1f, 0f, 1f).setOnUpdate((float value) => {
            GameOverScreen.GetComponent<CanvasGroup>().alpha = value;
        }).setOnComplete(() => {
            EnemyManager.Instance.waveIndex = 0;
            EnemyManager.Instance.MapTime = 0f;
            EnemyManager.Instance.RunMap = true;
            QuitButton.color = new Color(1f, 1f, 1f, 0f);
            RestartButton.color = new Color(1f, 1f, 1f, 0f);
            GameOverText.text = string.Empty;
            gameOverString = string.Empty;
            gameOverIndex = 0;
            winIndex = 0;
            GameOverText.gameObject.SetActive(false);
            GameOverScreen.SetActive(false);
            GameOverScreen.GetComponent<CanvasGroup>().alpha = 1f;
        });

        FadeController.FadeIn();
        UIHandler.Instance.UpdateHealth(5);
        Destroy(GameObject.Find("ShipExplosion"));
        ShipController.Instance.GetComponent<ShipEggInventory>().Eggs.Clear();
        ShipController.Instance.gameObject.SetActive(true);
        ShipController.Instance.transform.position = new Vector2(0f, -13.5f).To3D();
        ShipController.Instance.Health = 5;
    }

    void GameComplete() {
        FadeController.FadeOut();
        FadeController.SetColor(Color.white);
        WinScreen.SetActive(true);
        WinText.gameObject.SetActive(true);
    }
}