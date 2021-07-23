using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public delegate void OnShipDeathEvent();
    public event OnShipDeathEvent OnShipDeath;

    IEnumerator ShipDeath() {
        GlobalCanvas.Instance.FadeController.SetColor(Color.black);
        yield return new WaitForSeconds(3f);
        GlobalCanvas.Instance.FadeController.FadeOut();
        yield return new WaitForSeconds(2f);
        GlobalCanvas.Instance.GameOverText.gameObject.SetActive(true);
    }

    private void Awake() {
        if(!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GameOver() {
        StartCoroutine(ShipDeath());
        OnShipDeath?.Invoke();
    }
}