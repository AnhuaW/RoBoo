using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EECS494Helper : MonoBehaviour
{
    void Awake()
    {
        ConfigureGame();
    }

    void ConfigureGame()
    {
        /* The following two lines prevent your game from running at an unexpected speed. */
        /* 60 fps (frames-per-second) is perhaps the industry standard. */
        Debug.Log("Setting desired framerate to 60fps. Disabling vsync.");
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}
