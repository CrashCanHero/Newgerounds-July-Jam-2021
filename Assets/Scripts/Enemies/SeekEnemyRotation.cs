using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekEnemyRotation : MonoBehaviour {
    private void Update() {
        Vector2 dir = (ShipController.Instance.transform.position - transform.position).normalized;
        transform.eulerAngles = new Vector3(-90f, Vector3.SignedAngle(Vector3.forward, dir, Vector3.up), 180f);
        Debug.Log(Vector3.SignedAngle(Vector3.forward, dir, Vector3.up));
    }
}