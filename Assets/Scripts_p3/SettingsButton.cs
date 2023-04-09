using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] GameObject back_button = null;

    public void Settings()
    {
        SettingsMenu.instance.gameObject.SetActive(true);
        if (back_button != null)
        {
            back_button.SetActive(true);
        }
    }

    public void Back()
    {
        SettingsMenu.instance.gameObject.SetActive(false);
        if (back_button != null)
        {
            back_button.SetActive(false);
        }
    }
}
