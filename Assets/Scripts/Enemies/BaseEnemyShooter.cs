using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyShooter : MonoBehaviour {
    public float WaitTime;
    public float ShootSpeed;
    public GameObject bullet;

    public float timer;
    bool firstTimer = true;

    private void Update() {
        if(firstTimer) {
            timer += Time.deltaTime;
            if(timer >= WaitTime) {
                firstTimer = false;
                timer = 0f;
            }
            return;
        }

        timer += Time.deltaTime;
        if(timer >= ShootSpeed) {
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet bult = obj.GetComponent<Bullet>();
            bult.Angle = Vector2.SignedAngle((ShipController.Instance.transform.position - transform.position).To2D(), Vector2.up);
            timer = 0f;
        }
    }
}