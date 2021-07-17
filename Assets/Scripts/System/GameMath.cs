using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMath {
    public static Vector3 To3D(this Vector2 vec, float yLevel = 0f) {
        return new Vector3(vec.x, yLevel, vec.y);
    }

    public static Vector2 To2D(this Vector3 vec, float yLevel = 0f) {
        return new Vector2(vec.x, vec.z);
    }
}