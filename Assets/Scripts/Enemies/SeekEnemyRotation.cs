using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekEnemyRotation : MonoBehaviour {

    Vector3 rot;

    private void Awake() {
        rot = transform.localEulerAngles;
    }

    private void Update() {
        Vector2 dir = (ShipController.Instance.transform.position - transform.position).To2D().normalized;
        rot.y = -Vector2.SignedAngle(Vector3.forward.To2D(), dir);
        transform.localEulerAngles = rot;
    }
}