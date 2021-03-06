using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour {
    public static UIHandler Instance;

    [FoldoutGroup("Egg Info")] public RectTransform EggInfo;
    [FoldoutGroup("Egg Info")] public TMP_Text EggName, EggDescription;
    [FoldoutGroup("Egg Info")] public float EggDescriptionTime;
    [FoldoutGroup("Egg Info")] public Transform EggDisplayPivot;

    [FoldoutGroup("Text Box")] public TextConversation Conversation;
    [FoldoutGroup("Text Box")] public GameObject TextBox;
    [FoldoutGroup("Text Box")] public TMP_Text Text;
    [FoldoutGroup("Text Box")] public TMP_Text Name;
    [FoldoutGroup("Text Box")] string NextText;
    [FoldoutGroup("Text Box")] string currentText;
    [FoldoutGroup("Text Box")] int textIndex;
    [FoldoutGroup("Text Box")] int lineIndex;

    [FoldoutGroup("Score")] public TMP_Text ScoreText;
    [FoldoutGroup("Score")] public ulong Score;

    [FoldoutGroup("Credits")] public GameObject Credits;
    [FoldoutGroup("Credits")] public RectTransform CreditsPivot;
    [FoldoutGroup("Credits")] public Light SunLight;

    [FoldoutGroup("Pause Screen")] public GameObject PauseHolder;

    public TMP_Text HealthText;
    public GameObject GameHolder;

    public bool InCutscne => TextBox.activeSelf;

    public delegate void OnLastTextBoxEvent();
    public event OnLastTextBoxEvent OnLastTextBox;

    bool eggDescActive;
    float eggTimer;
    ulong visualScore;
    bool paused;

    private void Awake() {
        if(Instance) {
            return;
        }

        Instance = this;
        GameManager.Instance.OnSceneUnload += Unload;

        OnLastTextBox += LastTextBox;
    }

    private void Start() {
        if(!PlayerPrefs.HasKey("Tutorial")) {
            ShowText(Conversation);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else {
            EnemyManager.Instance.RunMap = true;
        }
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Escape) && !GlobalCanvas.Instance.EndSequence) {
            CyclePaused();
        }

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
                    OnLastTextBox?.Invoke();
                }
                else {
                    currentText = string.Empty;
                    NextText = Conversation.Lines[lineIndex].Line;
                    Name.text = Conversation.Lines[lineIndex].Name;
                    textIndex = 0;
                }
            }
        }

        if(visualScore < Score) {
            visualScore += (ulong)Mathf.CeilToInt((Score - visualScore) / 10f);

            ScoreText.text = visualScore.ToString();
        }

        if(!eggDescActive) {
            return;
        }

        eggTimer += Time.deltaTime;

        if(eggTimer >= 3f) {
            LeanTween.move(EggInfo, new Vector3(400f, EggInfo.anchoredPosition.y), 1f).setEaseInSine();
            eggDescActive = false;
        }
    }

    public void ShowEggInfo(Egg egg) {
        EggName.text = egg.Infos[egg.type].Name;
        EggDescription.text = egg.Infos[egg.type].Description;

        if(EggDisplayPivot.childCount > 0) {
            Destroy(EggDisplayPivot.GetChild(0).gameObject);
        }

        GameObject obj = Instantiate(egg.gameObject, EggDisplayPivot);
        obj.transform.localPosition = Vector3.zero;
        obj.GetComponent<Egg>().State = EggState.Display;
        obj.transform.GetChild(0).gameObject.layer = 7;

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

    public void ShowCredits() {
        GameHolder.SetActive(false);
        Credits.SetActive(true);
        SunLight.color = new Color(1f, 0.5f, 0f, 1f);

        //I have failed all who teach me and this is the reason why
        LeanTween.move(CreditsPivot, new Vector3(0f, 3600f, 0f), 80f).setOnComplete(() => {
            LeanTween.value(1f, 1f, 5f).setOnComplete(() => {
                GlobalCanvas.Instance.FadeController.FadeOut();
                LeanTween.value(1f, 1f, 1f).setOnComplete(() => {
                    GlobalCanvas.Instance.QuitGame();
                });
            });
        });
    }

    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
    }

    public void CyclePaused() {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;

        GameHolder.SetActive(!paused);
        PauseHolder.SetActive(paused);
    }
    void LastTextBox() {
        EnemyManager.Instance.RunMap = true;
    }

    void Unload(UnityEngine.SceneManagement.Scene scene) {
        if(scene.name != "Game") {
            return;
        }
        Instance = null;
        GameManager.Instance.OnSceneUnload -= Unload;
    }
}