using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float Speed;
    public float LifeTime;
    public float Angle;

    private void Start() {
        transform.eulerAngles = new Vector3(0f, Angle, 0f);
    }

    private void Update() {
        transform.position += transform.forward * Speed * Time.deltaTime;

        LifeTime -= Time.deltaTime;

        if(LifeTime <= 0f) {
            Destroy(gameObject);
        }
    }
}