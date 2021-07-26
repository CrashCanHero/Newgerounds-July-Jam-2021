using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpin : MonoBehaviour {
    public Transform ArtPivot;
    public float SpinSpeed;
    public Vector3 Offset;

    Vector3 rot;

    private void Update() {
        rot.y += SpinSpeed * Time.deltaTime;
        ArtPivot.localEulerAngles = rot;
    }
}