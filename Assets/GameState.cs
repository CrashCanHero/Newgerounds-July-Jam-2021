using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
