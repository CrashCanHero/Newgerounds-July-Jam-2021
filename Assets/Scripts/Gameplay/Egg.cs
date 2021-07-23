using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EggState {
    Active,
    Held,
    Using,
    Used,
    Display
}

public class Egg : MonoBehaviour {
    readonly Vector3 movement = new Vector3(0f, 0f, -8f);
    readonly Vector3 spin = new Vector3(0f, 0f, 25f);

    public EggStats[] Stats;
    public EggState State = EggState.Active;
    public Transform EggPivot;
    public bool AutoDestroy;
    [HideInInspector]public int type;

    TrailRenderer trail;

    private void OnTriggerEnter(Collider other) {
        if(!other.GetComponent<ShipController>() || State != EggState.Active) {
            return;
        }

        other.GetComponent<ShipEggInventory>().AttachEgg(gameObject);
        UIHandler.Instance.ShowEggInfo(this);
        trail.enabled = true;
        State = EggState.Held;
    }

    private void Start() {
        if(State == EggState.Display) {
            return;
        }
        type = Random.Range(0, Stats.Length);
        transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_BaseMap", Stats[type].Texture);
        trail = GetComponent<TrailRenderer>();
    }

    private void Update() {
        switch(State) {
            case EggState.Active:
            transform.position += movement * Time.deltaTime;
            break;
            case EggState.Using:
            OnUseUpdate();
            break;
            case EggState.Used:
            OnUsed();

            if(AutoDestroy) {
                Destroy(gameObject);
            }
            break;
            case EggState.Display:
            EggPivot.eulerAngles += spin * Time.deltaTime;
            break;
        }
    }

    public virtual void OnUseUpdate() { }
    public virtual void OnUsed() { }
}

[System.Serializable]
public struct EggStats {
    public EggInfo Info;
    public Texture2D Texture;
}