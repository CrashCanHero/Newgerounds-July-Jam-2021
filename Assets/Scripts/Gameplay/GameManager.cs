using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public int EnemyCount;

    public delegate void OnShipDeathEvent();
    public delegate void OnLevelCompleteEvent();
    public event OnShipDeathEvent OnShipDeath;
    public event OnLevelCompleteEvent OnLevelComplete;

    int lastEnemyCount;

    IEnumerator ShipDeath() {
        GlobalCanvas.Instance.GameOverScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
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
    }

    private void Update() {
        if(lastEnemyCount != EnemyCount) {
            if(EnemyManager.Instance && !EnemyManager.Instance.RunMap && EnemyCount == 0) {
                OnLevelComplete?.Invoke();
            }
            lastEnemyCount = EnemyCount;
        }
    }

    public void GameOver() {
        StartCoroutine(ShipDeath());
        OnShipDeath?.Invoke();
    }
}