using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemySpin : MonoBehaviour {
    public Transform ArtPivot;
    public float SpinSpeed;

    Vector3 rot;

    private void Update() {
        rot.x += SpinSpeed * Time.deltaTime;
        ArtPivot.eulerAngles = rot;
    }
}