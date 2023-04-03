using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PlayerControlsPanel;
    levelLoder level_loader = null;
    GameObject player;


    private void Awake()
    {
        GameObject level_loader_obj = GameObject.Find("levelLoader");
        if (level_loader_obj)
        {
            level_loader = level_loader_obj.GetComponent<levelLoder>();
        }

        player = GameObject.Find("Player");

    }


    public void Pause()
    {
        PlayerControlsPanel.SetActive(false);
        PauseMenu.SetActive(true);

        // freeze time-based processes
        Time.timeScale = 0; // game time is 0 * realtime

        // disable player input
        player.GetComponent<ArrowKeyMovement>().player_control = false;

        // disable collectible movement
        CollectibleMovement(false);
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;

        // ensable player input
        player.GetComponent<ArrowKeyMovement>().player_control = true;

        // enable collectible movement
        CollectibleMovement(true);
    }

    public void Restart()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        player.GetComponent<GameStatus>().has_checked = false;
        EventBus.Publish<GameOver>(new GameOver(true));
    }

    public void LoadCheckpoint()
    {
        PauseMenu.SetActive(false);

        EventBus.Publish<GameOver>(new GameOver(true));

        Time.timeScale = 1;
        // ensable player input
        player.GetComponent<ArrowKeyMovement>().player_control = true;
        // enable collectible movement
        CollectibleMovement(true);
    }

    public void PlayerControls()
    {
        PlayerControlsPanel.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void Menu()
    {
        // clear inventory
        Inventory_tmp.instance.ClearAll();

        // load menu
        int menu_scene_id = SceneManager.GetSceneByName("Menu").buildIndex;
        if (level_loader)
        {
            StartCoroutine(level_loader.LoadLevel(menu_scene_id));
        }
        else
        {
            SceneManager.LoadScene(menu_scene_id);
        }

        Time.timeScale = 1;
    }


    // disable/enable collectible movement (e.g. ammos)
    void CollectibleMovement(bool enable)
    {
        // ammos
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("battery");
        foreach (GameObject ammo in ammos)
        {
            CollectibleMovement cm = ammo.GetComponent<CollectibleMovement>();
            if (cm)
            {
                cm.enabled = enable;
            }
        }
        // keys
        GameObject[] keys = GameObject.FindGameObjectsWithTag("key");
        foreach (GameObject key in keys)
        {
            CollectibleMovement cm = key.GetComponent<CollectibleMovement>();
            if (cm)
            {
                cm.enabled = enable;
            }
        }
    }
}
