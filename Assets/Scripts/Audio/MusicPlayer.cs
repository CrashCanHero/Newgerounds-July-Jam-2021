using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public static MusicPlayer Instance;
    public AudioSource IntroSource, LoopSource;

    public bool Intro = true;

    private void Awake() {
        if(!Instance) {
            Instance = this;
        }        
    }

    private void Update() {
        if(Intro) {
            if(!IntroSource.isPlaying) {
                IntroSource.Play();
            }

            if(IntroSource.time / IntroSource.clip.length >= 0.95f) {
                Intro = false;
                LoopSource.Play();
            }
            return;
        }

        if(LoopSource.time / LoopSource.clip.length >= 0.95f) {
            LoopSource.time = 0f;
        }
    }

    public void Stop() {
        IntroSource.Stop();
        LoopSource.Stop();
    }

    public void Play() {
        Intro = true;
        IntroSource.Play();
    }
}