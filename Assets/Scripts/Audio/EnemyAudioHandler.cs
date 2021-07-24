using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioHandler : MonoBehaviour {
    public static EnemyAudioHandler Instance;

    public AudioSource Explosion;
    public AudioSource Hit;

    private void Awake() {
        if(Instance) {
            return;
        }

        Instance = this;
        GameManager.Instance.OnSceneUnload += Unload;
    }

    public void PlayExplosion() {
        Explosion.Play();
    }

    public void PlayHit() {
        Hit.Play();
    }

    void Unload(UnityEngine.SceneManagement.Scene scene) {
        if(scene.name != "Game") {
            return;
        }
        Instance = null;
        GameManager.Instance.OnSceneUnload -= Unload;
    }
}