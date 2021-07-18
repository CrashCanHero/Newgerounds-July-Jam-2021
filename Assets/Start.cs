using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Newgrounds;
    public void Play() 
    {
        DontDestroyOnLoad(Newgrounds);
        SceneManager.LoadScene("Loading");
    }
}
