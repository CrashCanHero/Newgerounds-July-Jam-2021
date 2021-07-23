using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioHandler : MonoBehaviour {
    public static EnemyAudioHandler Instance;

    public AudioSource Explosion;
    public AudioSource Hit;

    private void Awake() {
        if(!Instance) {
            Instance = this;
        }
    }

    public void PlayExplosion() {
        Explosion.Play();
    }

    public void PlayHit() {
        Hit.Play();
    }
}