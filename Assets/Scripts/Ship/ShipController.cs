using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShipController : MonoBehaviour {
    public static ShipController Instance { get; private set; }

    public const int HORIZONTAL = 0;
    public const int VERTICAL = 1;
    public const int SHOOT = 2;
    public const int SLOW = 3;

    public float MoveSpeed;
    public float ShootTime;
    public GameObject Shot;
    public Transform ShotPivot;
    public Player player;
    public Vector2 Limits;
    [Space]
    public int Health;
    public float ITime;
    public float FlashTime;
    public Renderer FlashRenderer;

    public AudioSource Shoot, Hurt, Death;
    public GameObject DeathEffect;

    float shootTimer;
    float iTimer;
    float flashTimer;

    public Vector2 GetAxis {
        get {
            return new Vector2(player.GetAxisRaw(HORIZONTAL), player.GetAxisRaw(VERTICAL));
        }
    }

    private void Awake() {
        if(Instance) {
            return;
        }

        Instance = this;
        GameManager.Instance.OnSceneUnload += Unload;

        player = ReInput.players.GetPlayer(0);
    }

    private void OnTriggerEnter(Collider other) {
        if((other.GetComponent<EnemyHealth>() || other.GetComponent<EnemyBullet>()) && iTimer <= 0f) {
            Health--;
            Health = Mathf.Clamp(Health, 0, 5);
            CameraHandler.Instance.RequestShake(0.5f, 0.2f);
            UIHandler.Instance.UpdateHealth(Health);
            if(Health == 0) {
                Death.Play();
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                EnemyManager.Instance.RunMap = false;
                MusicPlayer.Instance.Stop();
                GameManager.Instance.GameOver();
                return;
            }

            Hurt.Play();
            iTimer = ITime;
        }
    }

    private void Update() {
        if(!Instance) {
            Debug.Log("AHA");
        }
        if(Health == 0) {
            return;
        }
        bool shooting = player.GetButton(SHOOT);
        transform.position += GetAxis.To3D() * MoveSpeed * Time.deltaTime * (player.GetButton(SLOW) ? 0.2f : 1f) * (UIHandler.Instance.InCutscne ? 0f : 1f) * (shooting ? 0.8f : 1f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Limits.x, Limits.x), 0f, Mathf.Clamp(transform.position.z, -Limits.y, Limits.y));

        if(player.GetButtonDown(SHOOT) && !UIHandler.Instance.InCutscne) {
            shootTimer = ShootTime;
        }

        if(shooting && shootTimer > ShootTime && !UIHandler.Instance.InCutscne) {
            Instantiate(Shot, ShotPivot.position, Quaternion.identity);
            Shoot.Stop();
            Shoot.Play();
            shootTimer = 0f;
        }
        shootTimer += Time.deltaTime;
        if(iTimer >= 0f) {
            flashTimer += Time.deltaTime;

            if(flashTimer >= FlashTime) {
                FlashRenderer.enabled = !FlashRenderer.enabled;
                flashTimer = 0f;
            }

            iTimer -= Time.deltaTime;
        }else {
            //horrible, horrible. NEVER DO THIS AGAIN
            FlashRenderer.enabled = true;
        }
    }

    void Unload(UnityEngine.SceneManagement.Scene scene) {
        if(scene.name != "Game") {
            return;
        }
        Instance = null;
        GameManager.Instance.OnSceneUnload -= Unload;
    }
}