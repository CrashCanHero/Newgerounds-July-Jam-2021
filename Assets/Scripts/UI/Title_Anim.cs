using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.Video;

public class Title_Anim : MonoBehaviour
{
    public int Target;
    public float[] Locations;
    public bool INIT;

    public LeanTweenType[] easing;
    public GameObject[] items_To_Animate;

    public GameObject MAIN;
    public GameObject back;
    void Start()
    {
        INIT = false;
        StartCoroutine("Anim");
    }
    private void Update()
    {
    }
    IEnumerator Anim() 
    {
        float approxH = Screen.height / 900f; 
        for (int i = 0; i <= items_To_Animate.Length - 1; i++) {
            if (i <= items_To_Animate.Length - 3)
            {
                LeanTween.move(items_To_Animate[i], new Vector3(items_To_Animate[i].transform.position.x, Locations[i] * approxH), 1f).setEase(easing[i]);
            }
            else 
            {
                LeanTween.alpha(items_To_Animate[i].GetComponent<Image>().rectTransform, 1f, 0.5f).setEase(easing[i]);
            }
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.65f);
        INIT = true;
    }
    public void Set()
    {
        if (INIT)
        {
            LeanTween.rotate(MAIN, new Vector3(0f, 0f, 90f), 0.5f).setEase(LeanTweenType.easeOutBack);
            LeanTween.rotate(back, new Vector3(0f, 0f, -90f), 0.65f).setEase(LeanTweenType.easeOutExpo);
        }
    }
    public void Set_Rev()
    {
        if (INIT) {
            LeanTween.rotate(MAIN, new Vector3(0f, 0f, 0f), 0.5f).setEase(LeanTweenType.easeOutBack);
            LeanTween.rotate(back, new Vector3(0f, 0f, 0f), 0.65f).setEase(LeanTweenType.easeOutExpo);
        }
    }
}
