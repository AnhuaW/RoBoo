using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// exsists throughout entire game
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    Subscription<BGMChange> bgm_change_subscription;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        bgm_change_subscription = EventBus.Subscribe<BGMChange>(_OnBGMChange);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // change audio source and play new BGM
    void _OnBGMChange(BGMChange e)
    {
        if (instance.GetComponent<AudioSource>().clip != e.BGM_clip)
        {
            instance.GetComponent<AudioSource>().clip = e.BGM_clip;
            instance.GetComponent<AudioSource>().Play();
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(bgm_change_subscription);
    }
}


public class BGMChange
{
    public AudioClip BGM_clip;
    public BGMChange(AudioClip _BGM_clip) { BGM_clip = _BGM_clip; }
}