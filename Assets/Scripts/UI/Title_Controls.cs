using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Controls : MonoBehaviour
{
    IEnumerator LoadNext() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Loading");
    }

    public void PlayGame() {
        StartCoroutine(LoadNext());
    }
}
