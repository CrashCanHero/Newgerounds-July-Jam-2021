using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemySpin : MonoBehaviour {
    public Transform ArtPivot;
    public float SpinSpeed;

    Vector3 rot;

    private void Awake() {
        rot = new Vector3(0f, SpinSpeed * Time.deltaTime, 0f);
    }

    private void Update() {
        ArtPivot.eulerAngles += rot;
    }
}