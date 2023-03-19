using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// set current level's BGM in inspector
// BGM manager will automatically switch BGM if necessary
public class LevelBGM : MonoBehaviour
{
    [SerializeField] AudioClip level_bgm;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Publish<BGMChange>(new BGMChange(level_bgm));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
