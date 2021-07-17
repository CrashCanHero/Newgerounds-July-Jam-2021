using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : EnemyMovement {
    public AnimationCurve Curve;

    public override void Move(float time) {
        transform.position += Dir.To3D() * Speed * UnityEngine.Time.deltaTime * Curve.Evaluate(time / Time);
    }
}