using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public AudioSource IntroSource, LoopSource;

    public bool Intro = true;

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
}