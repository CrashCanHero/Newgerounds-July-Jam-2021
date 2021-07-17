using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum EnemyWaveType {
    BasicEnemiesFromTopMiddle,
}

public class EnemyManager : MonoBehaviour {
    public EnemyWave[] Waves;

    IEnumerator SpawnWave(EnemyWave Wave) {
        float timer = 0f;
        int index = 0;

        while (index < Wave.Enemies.Length) {
            while(timer < Wave.Enemies[index].Time) {
                timer += Time.deltaTime;
                yield return null;
            }
            Instantiate(Wave.Enemies[index].Enemy, Wave.Enemies[index].Position.To3D(), Quaternion.identity);
            index++;
            yield return null;
        }
    }

    private void Awake() {
        //SpawnWave(EnemyWaveType.BasicEnemiesFromTopMiddle);
    }

    public void SpawnWave(EnemyWaveType Type) {
        StartCoroutine(SpawnWave(Waves[(int)Type]));
    }

    [Button]
    void SpawnTestWave() {
        SpawnWave(EnemyWaveType.BasicEnemiesFromTopMiddle);
    }
}