using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    public static SettingsMenu instance;

    public AudioMixer audioMixer;
    public SFXcontrol player_sfx;
    public List<BreakableTile> breakable_sfx = new List<BreakableTile>();

    Subscription<SFXRefChange> sfx_ref_change_subscription;
    [SerializeField] float sfx_volume = 0.5f;
    [SerializeField] float bgm_volume = 0.5f;


    private void Awake()
    {
        gameObject.transform.parent = null;

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


        sfx_ref_change_subscription = EventBus.Subscribe<SFXRefChange>(_OnSFXRefChange);

    }

    private void Start()
    {
        GetSFXRef();
        SetBGMVolume(0.5f);
        SetSFXVolume(0.5f);

        GetComponent<Canvas>().enabled = true;
        gameObject.SetActive(false);
    }


    public void SetBGMVolume(float volume)
    {
        bgm_volume = Mathf.Log10(volume) * 20;
        ChangeBGMVolume();
    }

    public void SetSFXVolume(float volume)
    {
        sfx_volume = volume;
        ChangeSFXVolume();
    }

    void ChangeBGMVolume()
    {
        audioMixer.SetFloat("BGMVolume", bgm_volume);
    }

    void ChangeSFXVolume()
    {
        if (player_sfx != null)
        {
            player_sfx.clipVolume = sfx_volume;
        }
        foreach (BreakableTile tile in breakable_sfx)
        {
            if (tile != null)
            {
                tile.destroy_sfx_volume = sfx_volume;
            }
        }
    }

    public void GetSFXRef()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player_sfx = player.GetComponent<SFXcontrol>();
        }

        GameObject[] breakables = GameObject.FindGameObjectsWithTag("bar");
        foreach (GameObject breakable in breakables)
        {
            BreakableTile t = breakable.GetComponent<BreakableTile>();
            if (t != null)
            {
                breakable_sfx.Add(t);
            }
        }
    }


    public void SetFullScreen(bool full)
    {
        Screen.fullScreen = full;
    }



    // when game restarts or reloads, obtain new references
    // to player and breakable tiles and set their sfx volumes
    void _OnSFXRefChange(SFXRefChange c)
    {
        GetSFXRef();
        ChangeSFXVolume();
        ChangeBGMVolume();
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(sfx_ref_change_subscription);
    }

}


public class SFXRefChange
{
    public SFXRefChange() { }
}
