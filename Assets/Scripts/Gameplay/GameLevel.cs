using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Egg Ranger/Level")]
public class GameLevel : ScriptableObject {
    public EnemyLevelWave[] Waves;
}

[System.Serializable]
public struct EnemyLevelWave {
    public EnemyWaveType Type;
    public Vector2 Offset;
    public float Time;
}