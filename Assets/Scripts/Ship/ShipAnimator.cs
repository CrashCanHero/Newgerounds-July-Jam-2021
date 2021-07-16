using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimator : MonoBehaviour {
    public float MaxDistanceDifference;
    public float Multiplier;

    public Transform LeftWing, RightWing;

    Vector3 lastPos;

    private void Update() {
        float diff = transform.position.y - lastPos.y;
        float percent = diff / MaxDistanceDifference;
        percent = Mathf.Clamp(percent, -1f, 1f);
        percent *= Multiplier;
        LeftWing.localEulerAngles = new Vector3(0f, 0f, -percent);
        RightWing.localEulerAngles = new Vector3(0f, 0f, percent);
    }

    private void LateUpdate() {
        lastPos = new Vector2(lastPos.x, Mathf.MoveTowards(lastPos.y, transform.position.y, Mathf.Abs(transform.position.y - lastPos.y) / 10f));
    }
}