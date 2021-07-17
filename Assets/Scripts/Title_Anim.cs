using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Video;

public class Title_Anim : MonoBehaviour
{
    public int Target;
    public int VideoFile;
    public bool isPlaying;
    public float[] Locations;

    public LeanTweenType[] easing;
    public GameObject[] items_To_Animate;

    public VideoPlayer Video;
    public VideoClip[] Backgrounds;
    void Start()
    {
        StartCoroutine("Anim");
        Video.clip = Backgrounds[0];
        Video.Pause();
        Video.frame = 0;
    }
    private void Update()
    {
        if (isPlaying && ((int)Video.frame) == Target) 
        {
            Video.Pause();
            isPlaying = false;
        }
    }
    public void setVideo()
    {
        if (!isPlaying) {
            int VideoF = VideoFile + 1;
            if (VideoF > Backgrounds.Length - 1)
            {
                VideoF = 0;
            }
            Target = ((int)Backgrounds[VideoF].frameCount) - 1;
            isPlaying = true;
            Video.clip = Backgrounds[VideoF];
            Video.Play();
            VideoFile = VideoF;
        }
    }
    IEnumerator Anim() 
    {
        for (int i = 0; i <= items_To_Animate.Length - 1; i++) {
            LeanTween.move(items_To_Animate[i], new Vector3(items_To_Animate[i].transform.position.x, Locations[i]), 1.5f).setEase(easing[i]);
            yield return new WaitForSeconds(0.75f);
            Debug.Log(i);
        }
    }
}
