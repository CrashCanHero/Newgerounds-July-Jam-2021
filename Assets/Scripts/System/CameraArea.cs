using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArea : MonoBehaviour {
    public static Vector2 TopLeftCorner { get; private set; }
    public static Vector2 BottomRightCorner { get; private set; }

    readonly Vector3 TopLeftCheck = new Vector3(0f, 0f, 30f);
    readonly Vector3 BottomRightCheck = new Vector3(1f, 1f, 30f);

    Camera cam;

    private void Awake() {
        cam = GetComponent<Camera>();
    }

    private void Update() {
        TopLeftCorner = cam.ViewportToWorldPoint(TopLeftCheck);
        BottomRightCorner = cam.ViewportToWorldPoint(BottomRightCheck);
    }

    private void OnDrawGizmosSelected() {
        if(!Application.isPlaying) {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(TopLeftCorner, 0.1f);
        Gizmos.DrawSphere(BottomRightCorner, 0.1f);
    }
}