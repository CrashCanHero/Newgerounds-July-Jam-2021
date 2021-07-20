using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;




public class NGHelper : MonoBehaviour
{
    public io.newgrounds.core ngio_core;
    public io.newgrounds.objects.user userNG;
    public TMP_Text playerText;
    public Image pfp;
    public bool isBrowser;
    void Start()
    {
        ngio_core.onReady(() =>
        {

            ngio_core.checkLogin((bool logged_in) =>
            {
                if (logged_in)
                {
                    onLoggedIn();
                }
                else
                {
                    requestLogin();
                }
            });
        });
    }

    private void requestLogin()
    {
        ngio_core.requestLogin(onLoggedIn, onLoginFailed, onLoginCancelled);
    }

    private void onLoginCancelled()
    {
        //something
    }

    private void onLoginFailed()
    {
        io.newgrounds.objects.error err = ngio_core.login_error;
    }

    private void onLoggedIn()
    {
        userNG = ngio_core.current_user;
        playerText.text = userNG.name;
        io.newgrounds.components.ScoreBoard.getScores userScore = new io.newgrounds.components.ScoreBoard.getScores();
        userScore.id = 0;
        userScore.user = userNG.id;
        userScore.social = false;
        userScore.period = "A";
        userScore.callWith(ngio_core);
        StartCoroutine(GetTexture(userNG.icons.large));
    }
    public void unlockMedal(int MedalID)
    {
        io.newgrounds.components.Medal.unlock newMedal = new io.newgrounds.components.Medal.unlock();
        newMedal.id = MedalID;
        newMedal.callWith(ngio_core);
    }
    public void uploadScore(int ID, int Score)
    {
        io.newgrounds.components.ScoreBoard.postScore upload = new io.newgrounds.components.ScoreBoard.postScore();
        upload.id = ID;
        upload.value = Score;
        upload.callWith(ngio_core);
    }
    IEnumerator GetTexture(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Is Browser");
            isBrowser = true;
            pfp.enabled = false;
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = SpriteFromTexture2D(myTexture);
            pfp.sprite = sprite;
        }
    }
    Sprite SpriteFromTexture2D(Texture2D texture)
    {

        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}