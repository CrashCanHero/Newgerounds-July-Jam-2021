using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Egg Ranger/Enemy Wave")]
public class EnemyWave : ScriptableObject {
    public EnemySpawn[] Enemies;
}

[System.Serializable]
public struct EnemySpawn {
    public GameObject Enemy;
    public Vector2 Position;
    public float Time;
}