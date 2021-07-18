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

    float shootTimer;

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

    private void Update() {
        transform.position += GetAxis.To3D() * MoveSpeed * Time.deltaTime * (player.GetButton(SLOW) ? 0.2f : 1f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Limits.x, Limits.x), 0f, Mathf.Clamp(transform.position.z, -Limits.y, Limits.y));

        if(player.GetButtonDown(SHOOT)) {
            shootTimer = ShootTime;
        }

        if(player.GetButton(SHOOT) && shootTimer > ShootTime) {
            Instantiate(Shot, ShotPivot.position, Quaternion.identity);
            shootTimer = 0f;
        }
        shootTimer += Time.deltaTime;
    }
}