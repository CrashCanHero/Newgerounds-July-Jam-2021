using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : EnemyMovement {
    public override void Move(float time) {
        transform.position += Dir.To3D() * Speed * UnityEngine.Time.deltaTime;
    }
}