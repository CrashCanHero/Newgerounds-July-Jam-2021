using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
    public float Time;

    float timer;

    private void Update() {
        timer += UnityEngine.Time.deltaTime;

        if(timer >= Time) {
            Destroy(gameObject);
        }
    }
}