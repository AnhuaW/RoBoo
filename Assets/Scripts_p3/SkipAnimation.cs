using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipAnimation : MonoBehaviour
{
    [SerializeField] int next_scene_index = 1;
    [SerializeField] VideoPlayer video;
    levelLoder loader = null;

    private void Start()
    {
        loader = GameObject.Find("levelLoader").GetComponent<levelLoder>();
        video.loopPointReached += OnVideoEnd;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            video.Stop();
            StartCoroutine(loader.LoadLevel(next_scene_index));
        }
    }


    void OnVideoEnd(VideoPlayer v)
    {
        StartCoroutine(loader.LoadLevel(next_scene_index));
    }
}
