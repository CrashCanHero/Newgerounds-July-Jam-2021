using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShipController : MonoBehaviour {
    public static ShipController Instance;

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
        if(!Instance) {
            Instance = this;
        }
        player = ReInput.players.GetPlayer(0);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<EnemyHealth>() && iTimer <= 0f) {
            Health--;
            Health = Mathf.Clamp(Health, 0, 5);

            if(Health == 0) {
                Death.Play();
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                return;
            }

            Hurt.Play();
            iTimer = ITime;

            CameraHandler.Instance.RequestShake(0.5f, 0.2f);
            UIHandler.Instance.UpdateHealth(Health);
        }
    }

    private void Update() {
        if(Health == 0) {
            return;
        }
        transform.position += GetAxis.To3D() * MoveSpeed * Time.deltaTime * (player.GetButton(SLOW) ? 0.2f : 1f) * (UIHandler.Instance.InCutscne ? 0f : 1f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Limits.x, Limits.x), 0f, Mathf.Clamp(transform.position.z, -Limits.y, Limits.y));

        if(player.GetButtonDown(SHOOT) && !UIHandler.Instance.InCutscne) {
            shootTimer = ShootTime;
        }

        if(player.GetButton(SHOOT) && shootTimer > ShootTime && !UIHandler.Instance.InCutscne) {
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
}