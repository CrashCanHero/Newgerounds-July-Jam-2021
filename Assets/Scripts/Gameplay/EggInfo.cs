using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Egg Ranger/Egg Info")]
public class EggInfo : ScriptableObject {
    public string Name;
    [Multiline(6)]public string Description;
}