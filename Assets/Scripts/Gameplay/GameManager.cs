using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Toolnity;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public int EnemyCount;

    public delegate void OnShipDeathEvent();
    public delegate void OnLevelCompleteEvent();
    public delegate void OnSceneUnloadEvent(Scene scene);
    public event OnShipDeathEvent OnShipDeath;
    public event OnLevelCompleteEvent OnLevelComplete;
    public event OnSceneUnloadEvent OnSceneUnload;

    int lastEnemyCount;

    IEnumerator ShipDeath() {
        GlobalCanvas.Instance.GameOverScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        GlobalCanvas.Instance.FadeController.FadeOut();
        yield return new WaitForSeconds(2f);
        GlobalCanvas.Instance.GameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        LeanTween.alpha(GlobalCanvas.Instance.QuitButton.GetComponent<RectTransform>(), 1f, 1.6f);
        yield return new WaitForSeconds(0.4f);
        LeanTween.alpha(GlobalCanvas.Instance.RestartButton.GetComponent<RectTransform>(), 1f, 1.6f);
    }

    IEnumerator LevelComplete() {
        GlobalCanvas.Instance.WinScreen.SetActive(true);
        GlobalCanvas.Instance.FadeController.FadeOut();
        yield return new WaitForSeconds(1.2f);
        GlobalCanvas.Instance.WinText.gameObject.SetActive(true);
    }

    private void Awake() {
        if(!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        SceneManager.sceneUnloaded += SceneUnload;
    }

    private void Update() {
        if(lastEnemyCount != EnemyCount) {
            if(EnemyManager.Instance && !EnemyManager.Instance.RunMap && EnemyCount == 0) {
                try
                {
                    NGHelper.Instance.unlockMedal(64394);
                }
                catch (Exception e) 
                {
                    Debug.Log(e.Message);
                }
                OnLevelComplete?.Invoke();
            }
            lastEnemyCount = EnemyCount;
        }
    }

    public void GameOver() {
        StartCoroutine(ShipDeath());
        OnShipDeath?.Invoke();
    }

    void SceneUnload(Scene scene) {
        OnSceneUnload?.Invoke(scene);
    }
}