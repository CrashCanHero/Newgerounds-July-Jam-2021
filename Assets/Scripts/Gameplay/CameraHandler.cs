using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public static CameraHandler Instance;
    public Transform Target;
    public float ParallaxAmount;
    public bool Shake;

    Vector3 pos;
    float shakeTime;
    float shakeAmount;

    private void Awake() {
        if(Instance) {
            return;
        }

        Instance = this;
        GameManager.Instance.OnSceneUnload += Unload;

        pos = transform.position;
    }

    private void Update() {
        if(!Target) {
            return;
        }

        pos.x = Target.position.x / ParallaxAmount;

        if(Shake) {
            pos += Random.insideUnitCircle.To3D() * shakeAmount;

            shakeTime -= Time.deltaTime;

            if(shakeTime <= 0f) {
                Shake = false;
            }
        }

        transform.position = pos;
    }

    public void RequestShake(float ShakeAmount, float Time) {
        shakeTime = Time;
        shakeAmount = ShakeAmount;
        Shake = true;
    }

    void Unload(UnityEngine.SceneManagement.Scene scene) {
        if(scene.name != "Game") {
            return;
        }
        Instance = null;
        GameManager.Instance.OnSceneUnload -= Unload;
    }
}