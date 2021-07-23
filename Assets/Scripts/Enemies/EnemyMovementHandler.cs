using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour {
    public EnemyMovement[] Movements;
    public bool Loop;

    int index = 0;
    float timer = 0;
    bool run = true;

    private void Start() {
        if(Movements.Length > 0) {
            Movements[index].OnStart();
        }
    }

    private void Update() {
        if(run && Movements.Length > 0) {
            timer += Time.deltaTime;
            Movements[index].Move(timer);
            if(timer >= Movements[index].Time) {
                if(Movements[index].OneShot) {
                    Movements[index].Time = 0f;
                }
                index++;

                if(index == Movements.Length) {
                    if(Loop) {
                        index = 0;
                    }
                    else {
                        run = false;
                    }
                }
                Movements[index].OnStart();
                timer = 0f;
            }
        }
    }
}