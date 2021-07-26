using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum EnemyWaveType {
    SingleBase,
    SingleSeek,
    SingleSaw,
    SingleShooter,
    BasicEnemiesFromTopMiddle,
    SeekEnemiesFromTop,
    SawEnemiesFromTopLeft,
    ShooterEnemiesFromTop,
    Egg,
    SingleSideFromLeft,
    SingleSideFromRight,
    SideFromLeftBottomFirst,
    SideFromRightBottomFirst,
    SideFromLeftTopFirst,
    SideFromRIghtTopFirst
}

public class EnemyManager : MonoBehaviour {
    public static EnemyManager Instance;
    public EnemyWave[] Waves;
    public GameLevel CurrentLevel;
    public float MapTime;
    public bool RunMap;

    [HideInInspector]public int waveIndex;

    IEnumerator SpawnWave(EnemyWave Wave, Vector2 Offset) {
        float timer = 0f;
        int index = 0;

        while (index < Wave.Enemies.Length) {
            while(timer < Wave.Enemies[index].Time) {
                timer += Time.deltaTime;
                yield return null;
            }
            Instantiate(Wave.Enemies[index].Enemy, Wave.Enemies[index].Position.To3D() + Offset.To3D(), Quaternion.identity);
            index++;
            yield return null;
        }
    }

    private void Awake() {
        if(Instance) {
            return;
        }

        Instance = this;
        GameManager.Instance.OnSceneUnload += Unload;
    }

    private void Update() {
        if(!CurrentLevel|| !RunMap) {
            return;
        }

        MapTime += Time.deltaTime;

        if(MapTime >= CurrentLevel.Waves[waveIndex].Time) {
            SpawnWave(CurrentLevel.Waves[waveIndex].Type, CurrentLevel.Waves[waveIndex].Offset);
            waveIndex++;

            if(waveIndex == CurrentLevel.Waves.Length) {
                RunMap = false;
            }
        }
    }

    public void SpawnWave(EnemyWaveType Type, Vector2 Offset) {
        StartCoroutine(SpawnWave(Waves[(int)Type], Offset));
    }

    void Unload(UnityEngine.SceneManagement.Scene scene) {
        if(scene.name != "Game") {
            return;
        }
        Instance = null;
        GameManager.Instance.OnSceneUnload -= Unload;
    }

    [Button]
    void SpawnTestWave() {
        SpawnWave(EnemyWaveType.BasicEnemiesFromTopMiddle, Vector2.zero);
    }
}