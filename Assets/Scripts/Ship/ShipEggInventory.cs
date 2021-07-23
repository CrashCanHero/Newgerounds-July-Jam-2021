using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEggInventory : MonoBehaviour {
    public float EggDensity;
    public Transform EggPivot;
    public Transform EggHolder;
    public List<GameObject> Eggs = new List<GameObject>();

    public List<Vector2> PrevShipPos;

    private void Awake() {
        for(int i = 0; i < 60; i++) {
            PrevShipPos.Add(transform.position.To2D());
        }
    }

    private void Update() {
        UpdateShipPositionList();
    }

    private void LateUpdate() {
        for(int i = Eggs.Count - 1; i > -1; i--) {
            int target = Mathf.RoundToInt(((float)(i + 1) / Eggs.Count) * PrevShipPos.Count - 1);
            Vector3 targetPos = PrevShipPos[target].To3D() + new Vector3(0f, 0f, ((Eggs.Count - 1) - i) * EggDensity);
            Vector3 targetEgg = Eggs[(Eggs.Count - 1) - i].transform.position;

            Eggs[(Eggs.Count - 1) - i].transform.position = Vector3.MoveTowards(targetEgg, targetPos, (Vector3.Distance(targetEgg, targetPos) / 10f));
        }
    }

    public void AttachEgg(GameObject egg) {
        Eggs.Add(egg);
        egg.transform.parent = EggHolder;
    }

    public void UpdateShipPositionList() {
        PrevShipPos.Remove(PrevShipPos[0]);
        PrevShipPos.Add(EggPivot.position.To2D());
    }
}