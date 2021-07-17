using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGHandler : MonoBehaviour {
    public Vector2 Limits;
    public Transform Repeater;
    public float Speed;

    Vector3 pos;

    private void Awake() {
        pos = new Vector3(0f, 0f, Limits.y);
        Instantiate(Repeater, transform.position + new Vector3(0f, 0f, 160f), Quaternion.identity, transform);
    }

    private void Update() {
        pos.z -= Speed * Time.deltaTime;

        if(pos.z <= Limits.x) {
            float diff = pos.z - Limits.x;
            pos.z = Limits.y + diff;
        }

        transform.position = pos;
    }
}