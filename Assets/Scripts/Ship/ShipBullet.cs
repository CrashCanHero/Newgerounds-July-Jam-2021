using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : Bullet {

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<EnemyHealth>()) {
            Destroy(gameObject);
        }
    }
}