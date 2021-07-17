using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemySpin : MonoBehaviour {
    public Transform ArtPivot;
    public float SpinSpeed;

    private void Update() {
        ArtPivot.eulerAngles = new Vector3(ArtPivot.eulerAngles.x, ArtPivot.eulerAngles.y + (SpinSpeed * Time.deltaTime), ArtPivot.eulerAngles.z);
    }
}