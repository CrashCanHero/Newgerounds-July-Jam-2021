using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemyPositionSet : MonoBehaviour {
    public bool Right;

    private void Awake() {
        transform.position = new Vector3(Right ? CameraArea.BottomRightCorner.x + 5f : CameraArea.TopLeftCorner.x - 5f, transform.position.y, transform.position.z);

        if(Right) {
            foreach(EnemyMovement mov in GetComponents<EnemyMovement>()) {
                mov.Dir.x *= -1f;
            }
        }
    }
}