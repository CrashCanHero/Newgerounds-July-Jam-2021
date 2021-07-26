using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour {
    public CanvasGroup Group;
    public CanvasGroup[] Frames;
    public VideoPlayer player;
    public AudioSource Music;

    List<LTDescr> tweens = new List<LTDescr>();

    private void Awake() {
        tweens.Add(LeanTween.value(1f, 1f, 3.5f).setOnComplete(() => {
            tweens.Add(LeanTween.value(0f, 1f, 1.5f).setOnUpdate((float value) => {
                Group.alpha = value;
            }));
            tweens.Add(LeanTween.value(1f, 0f, 1f).setOnUpdate((float value) => {
                player.targetCameraAlpha = value;
            }).setOnComplete(() => {
                Music.Play();
                tweens.Add(LeanTween.value(0f, 1f, 1.5f).setOnUpdate((float value) => {
                    Frames[0].alpha = value;
                }).setOnComplete(() => {
                    tweens.Add(LeanTween.value(0f, 1f, 1.5f).setOnUpdate((float value) => {
                        Frames[1].alpha = value;
                    }).setOnComplete(() => {
                        tweens.Add(LeanTween.value(0f, 1f, 1.5f).setOnUpdate((float value) => {
                            Frames[2].alpha = value;
                        }).setOnComplete(() => {
                            tweens.Add(LeanTween.value(0f, 1f, 1.5f).setOnUpdate((float value) => {
                                Frames[3].alpha = value;
                            }).setOnComplete(() => {
                                tweens.Add(LeanTween.value(0f, 1f, 1.5f).setOnUpdate((float value) => {
                                    Frames[4].alpha = value;
                                }));
                            }));
                        }));
                    }));
                }));
            }));
        }));
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            LeanTween.cancelAll();
            SceneManager.LoadScene("MainMenu");
        }
    }
}