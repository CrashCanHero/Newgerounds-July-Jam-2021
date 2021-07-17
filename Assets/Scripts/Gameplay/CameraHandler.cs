using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
    public Transform Target;
    public float ParallaxAmount;

    private void Update() {
        if(!Target) {
            return;
        }

        transform.position = new Vector3(Target.position.x / ParallaxAmount, 30f, 0f);
    }
}