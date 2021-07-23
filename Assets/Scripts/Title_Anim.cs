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
    public float[] Locations_Stats_Out;
    public float[] Locations_Stats_In;

    public LeanTweenType[] easing;
    public LeanTweenType[] easing_Stats;
    public GameObject[] items_To_Animate;
    public GameObject[] items_To_Animate_Stats;

    public GameObject back;
    void Start()
    {
        StartCoroutine("Anim");
    }
    private void Update()
    {
    }
    public void Stats()
    {
        StartCoroutine("StatsAnim");
    }
    IEnumerator StatsAnim() 
    {
        float approxH = Screen.height / 900f;
        for (int i = 0; i <= items_To_Animate.Length - 2; i++)
        {
            if (i == 0 || i > 3)
            {
                LeanTween.move(items_To_Animate[i], new Vector3(items_To_Animate[i].transform.position.x, Locations_Stats_Out[i] * approxH), 1f).setEase(easing[i]);
            }
            else
            {
                LeanTween.alpha(items_To_Animate[i].GetComponent<Image>().rectTransform, 0f, 0.75f).setEase(easing[i]);
            }
            yield return new WaitForSeconds(0.5f);
        }
        LeanTween.rotate(back, new Vector3(0,0,90), 0.5f).setEase(LeanTweenType.easeInOutBack);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i <= items_To_Animate_Stats.Length - 1; i++)
        {
            LeanTween.move(items_To_Animate_Stats[i], new Vector3(items_To_Animate_Stats[i].transform.position.x, Locations_Stats_In[i] * approxH), 1f).setEase(easing_Stats[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Anim() 
    {
        float approxH = Screen.height / 900f; 
        for (int i = 0; i <= items_To_Animate.Length - 1; i++) {
            if (i == 0 || i > 3)
            {
                LeanTween.move(items_To_Animate[i], new Vector3(items_To_Animate[i].transform.position.x, Locations[i] * approxH), 1.5f).setEase(easing[i]);
            }
            else 
            {
                LeanTween.alpha(items_To_Animate[i].GetComponent<Image>().rectTransform, 1f, 0.75f).setEase(easing[i]);
            }
            yield return new WaitForSeconds(0.75f);
        }
    }
}
