using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGHandler : MonoBehaviour {
    public Vector2 Limits;
    public Transform Repeater;
    public Transform Clouds;
    public float Speed;
    public float ShipMultiplier;

    Vector3 pos;
    Vector3 cloudPos;

    private void Awake() {
        pos = new Vector3(0f, 0f, Limits.y);
        cloudPos = new Vector3();
        Instantiate(Repeater, transform.position + new Vector3(0f, 0f, 160f), Quaternion.identity, transform);
    }

    private void Update() {
        ShipMultiplier = (ShipController.Instance.transform.position.z + 15f) / 30f;
        pos.z -= Speed * Time.deltaTime;
        cloudPos.z -= Speed * Time.deltaTime * 2f;

        if(pos.z <= Limits.x) {
            float diff = pos.z - Limits.x;
            pos.z = Limits.y + diff;
            cloudPos.z = 0f;
        }

        transform.position = pos;
        Clouds.position = cloudPos;
    }
}