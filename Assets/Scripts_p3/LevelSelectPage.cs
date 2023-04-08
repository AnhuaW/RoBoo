using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPage : MonoBehaviour
{
    [SerializeField] GameObject LevelSelector;
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject QuitButton;
    [SerializeField] GameObject SettingsButton;

    levelLoder level_loader = null;
    private void Awake()
    {
        GameObject level_loader_obj = GameObject.Find("levelLoader");
        if (level_loader_obj)
        {
            level_loader = level_loader_obj.GetComponent<levelLoder>();
        }
    }

    public void OpenLevelSelector()
    {
        RenderLevelSelector(true);
    }

    public void CloseLevelSelector()
    {
        RenderLevelSelector(false);
    }


    void RenderLevelSelector(bool open)
    {
        LevelSelector.SetActive(open);
        gameObject.SetActive(!open);
        PlayButton.SetActive(!open);
        QuitButton.SetActive(!open);
        SettingsButton.SetActive(!open);
    }


}
