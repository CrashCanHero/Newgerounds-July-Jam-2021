using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardPlayerMovement : EnemyMovement {
    public override void Move(float time) {
        Dir = (ShipController.Instance.transform.position - transform.position).normalized.To2D();
        transform.position += Dir.To3D() * Speed * UnityEngine.Time.deltaTime;
    }
}