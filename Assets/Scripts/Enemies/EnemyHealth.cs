using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int Health;
    public Renderer rend;
    public GameObject DeathEffect;
    public GameObject HealthUp;
    public ulong ScoreToAdd;
    Material material;

    IEnumerator FlashOnHit() {
        material.SetFloat("_DelaunayStrength1", 100f);
        material.SetFloat("_DelaunayStrength2", 100f);
        yield return new WaitForSeconds(0.2f);
        material.SetFloat("_DelaunayStrength1", 2.41f);
        material.SetFloat("_DelaunayStrength2", 3.9f);
    }

    private void Awake() {
        GameManager.Instance.EnemyCount++;
        material = rend.material;
    }

    private void OnDestroy() {
        GameManager.Instance.EnemyCount--;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<ShipBullet>()) {
            Health--;
            StartCoroutine(FlashOnHit());
            EnemyAudioHandler.Instance.PlayHit();
            if(Health <= 0) {
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                EnemyAudioHandler.Instance.PlayExplosion();
                CameraHandler.Instance.RequestShake(0.1f, 0.1f);
                UIHandler.Instance.Score += ScoreToAdd;

                int rand = Random.Range(0, 100);

                if(Mathf.Abs(rand - 30) < 5) {
                    Instantiate(HealthUp, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
    }
}