using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public Vector2 Dir;
    public float Time;
    public float Speed;
    public bool OneShot;

    public virtual void Move(float time) { }
}