using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Test_Load : MonoBehaviour
{
    public TMP_Text text;
    public string scene;
    AsyncOperation load;

    bool visuals;

    IEnumerator FadeLevel() {
        yield return new WaitForSeconds(1f);
        GlobalCanvas.Instance.FadeController.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync("Loading");
        GlobalCanvas.Instance.FadeController.FadeIn();
    }

    void Start()
    {
        GlobalCanvas.Instance.FadeController.FadeIn();
        load = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }

    void Update()
    {
        float percent = Mathf.Clamp01(load.progress / 0.9f);
        text.text = "Loading... " + Mathf.Round(percent * 100) + "%";

        if(percent >= 0.95f && !visuals) {
            StartCoroutine(FadeLevel());
            visuals = true;
        }
    }
}
