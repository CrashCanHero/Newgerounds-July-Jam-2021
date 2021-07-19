using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class UIHandler : MonoBehaviour {
    public static UIHandler Instance;

    [FoldoutGroup("Egg Info")] public RectTransform EggInfo;
    [FoldoutGroup("Egg Info")] public TMP_Text EggName, EggDescription;
    [FoldoutGroup("Egg Info")] public float EggDescriptionTime;

    [FoldoutGroup("TextBox")] public TextConversation Conversation;
    [FoldoutGroup("TextBox")] public GameObject TextBox;
    [FoldoutGroup("TextBox")] public TMP_Text Text;
    [FoldoutGroup("TextBox")] public TMP_Text Name;
    [FoldoutGroup("TextBox")] string NextText;
    [FoldoutGroup("TextBox")] string currentText;
    [FoldoutGroup("TextBox")] int textIndex;
    [FoldoutGroup("TextBox")] int lineIndex;

    public TMP_Text HealthText;

    public bool InCutscne => TextBox.activeSelf;

    bool eggDescActive;
    float eggTimer;

    private void Awake() {
        if(!Instance) {
            Instance = this;
        }

        if(!PlayerPrefs.HasKey("Tutorial")) {
            ShowText(Conversation);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
    }

    private void Update() {

        if(TextBox.activeSelf) {
            if(textIndex < NextText.Length) {
                currentText += NextText[textIndex];
                textIndex++;
            }

            Text.text = currentText;

            if(Input.GetKeyDown(KeyCode.Return)) {
                lineIndex++;
                if(lineIndex >= Conversation.Lines.Length) {
                    TextBox.SetActive(false);
                }
                else {
                    currentText = string.Empty;
                    NextText = Conversation.Lines[lineIndex].Line;
                    Name.text = Conversation.Lines[lineIndex].Name;
                    textIndex = 0;
                }
            }
        }

        if(!eggDescActive) {
            return;
        }

        eggTimer += Time.deltaTime;

        if(eggTimer >= 3f) {
            LeanTween.move(EggInfo, new Vector3(400f, EggInfo.anchoredPosition.y), 1f).setEaseInSine();
            LeanTween.scale(EggName.gameObject, new Vector3(0f, 1f, 1f), 1f).setEaseInSine();
            eggDescActive = false;
        }
    }

    public void ShowEggInfo(string Name, string Description) {
        EggName.text = Name;
        EggDescription.text = Description;
        LeanTween.scale(EggName.gameObject, Vector3.one, 1f).setEaseOutCirc();
        LeanTween.move(EggInfo, new Vector3(0f, EggInfo.anchoredPosition.y), 1f).setEaseOutCirc().setOnComplete(() => {
            eggDescActive = true;
            eggTimer = 0f;
        });
    }

    public void ShowText(TextConversation Conversation) {
        textIndex = 0;
        lineIndex = 0;
        currentText = string.Empty;
        Name.text = Conversation.Lines[0].Name;
        NextText = Conversation.Lines[0].Line;
        TextBox.SetActive(true);
    }

    public void UpdateHealth(int health) {
        HealthText.text = string.Empty;

        for(int i = 0; i < health; i++) {
            HealthText.text += 'O';
        }
    }
}