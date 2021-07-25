using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;




public class NGHelper : MonoBehaviour {
    public static NGHelper Instance;
    public io.newgrounds.core Ngio_core;
    public io.newgrounds.objects.user UserNG;
    public TMP_Text PlayerText;
    public Image Pfp;
    public bool IsBrowser;

    private void Awake() {
        if(!Instance) {
            Instance = this;
        }
        else {
            Destroy(transform.parent.gameObject);
        }
    }

    void Start() {
        Ngio_core.onReady(() => {

            Ngio_core.checkLogin((bool logged_in) => {
                if(logged_in) {
                    onLoggedIn();
                }
                else {
                    requestLogin();
                }
            });
        });
    }

    private void requestLogin() {
        Ngio_core.requestLogin(onLoggedIn, onLoginFailed, onLoginCancelled);
    }

    private void onLoginCancelled() {
        //something
    }

    private void onLoginFailed() {
        io.newgrounds.objects.error err = Ngio_core.login_error;
    }

    private void onLoggedIn() {
        UserNG = Ngio_core.current_user;
        PlayerText.text = UserNG.name;
        io.newgrounds.components.ScoreBoard.getScores userScore = new io.newgrounds.components.ScoreBoard.getScores();
        userScore.id = 0;
        userScore.user = UserNG.id;
        userScore.social = false;
        userScore.period = "A";
        userScore.callWith(Ngio_core);
        StartCoroutine(GetTexture(UserNG.icons.large));
    }

    public void unlockMedal(int MedalID) {
        io.newgrounds.components.Medal.unlock newMedal = new io.newgrounds.components.Medal.unlock();
        newMedal.id = MedalID;
        newMedal.callWith(Ngio_core);
    }

    public void uploadScore(int ID, int Score) {
        io.newgrounds.components.ScoreBoard.postScore upload = new io.newgrounds.components.ScoreBoard.postScore();
        upload.id = ID;
        upload.value = Score;
        upload.callWith(Ngio_core);
    }

    IEnumerator GetTexture(string url) {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Is Browser");
            IsBrowser = true;
            Pfp.enabled = false;
        }
        else {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = SpriteFromTexture2D(myTexture);
            Pfp.sprite = sprite;
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture) {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}