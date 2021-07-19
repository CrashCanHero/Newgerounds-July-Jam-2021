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
    void Start()
    {
        load = SceneManager.LoadSceneAsync(scene);
    }
    void Update()
    {
        float percent = Mathf.Clamp01(load.progress / 0.9f);
        text.text = "Loading... " + Mathf.Round(percent * 100) + "%";
    }
}
