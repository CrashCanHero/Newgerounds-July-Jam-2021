using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EggState {
    Active,
    Held,
    Using,
    Used
}

public class Egg : MonoBehaviour {
    readonly Vector3 movement = new Vector3(0f, 0f, -8f);

    public EggInfo Info;
    public EggState State = EggState.Active;
    public bool AutoDestroy;

    private void OnTriggerEnter(Collider other) {
        if(!other.GetComponent<ShipController>() || State != EggState.Active) {
            return;
        }

        other.GetComponent<ShipEggInventory>().AttachEgg(gameObject);
        UIHandler.Instance.ShowEggInfo(Info.Name, Info.Description);
        State = EggState.Held;
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
        }
    }

    public virtual void OnUseUpdate() { }
    public virtual void OnUsed() { }
}