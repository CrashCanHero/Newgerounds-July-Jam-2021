using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimator : MonoBehaviour {
    public float MaxDistanceDifference;
    public float Multiplier;

    public Transform ShipPivot;
    public Transform LeftWing, RightWing;

    Vector3 lastPos;

    private void Update() {
        float diff = transform.position.z - lastPos.z;
        float percent = diff / MaxDistanceDifference;
        percent = Mathf.Clamp(percent, -1f, 1f);
        percent *= Multiplier;
        LeftWing.localEulerAngles = new Vector3(-percent, -percent * 2f, 0f);
        LeftWing.eulerAngles = new Vector3(LeftWing.eulerAngles.x, LeftWing.eulerAngles.y, -90f);
        RightWing.localEulerAngles = new Vector3(-percent, -percent * 2f, 0f);
        RightWing.eulerAngles = new Vector3(RightWing.eulerAngles.x, RightWing.eulerAngles.y, 90f);

        diff = transform.position.x - lastPos.x;
        percent = diff / MaxDistanceDifference;
        percent = Mathf.Clamp(percent, -1f, 1f);
        percent *= Multiplier;

        ShipPivot.transform.eulerAngles = new Vector3(0f, 180f, percent);



        lastPos = new Vector3(
            Mathf.MoveTowards(lastPos.x, transform.position.x, Mathf.Abs(transform.position.x - lastPos.x) / 10f), 
            lastPos.y, 
            Mathf.MoveTowards(lastPos.z, transform.position.z, Mathf.Abs(transform.position.z - lastPos.z) / 10f)
            );
    }
}