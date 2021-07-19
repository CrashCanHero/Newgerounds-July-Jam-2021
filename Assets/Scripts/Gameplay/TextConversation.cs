using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Egg Ranger/Covnersation")]
public class TextConversation : ScriptableObject {
    public TextLine[] Lines;
}

[System.Serializable]
public struct TextLine {
    public string Name;
    [Multiline(10)]public string Line;
}