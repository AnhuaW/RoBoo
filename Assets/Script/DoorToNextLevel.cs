using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNextLevel : MonoBehaviour
{
    public int current_level_index = 0;
    [SerializeField] int total_scenes_count = 0;
    GameObject load;
    public bool has_key_constraints = false;
    public int required_key = 3;
    Inventory_tmp inventory;

    // Start is called before the first frame update
    void Start()
    {
        load = GameObject.Find("levelLoader");
        inventory = Inventory_tmp.instance;
        //total_scenes_count = SceneManager.sceneCountInBuildSettings;
        CountTotalScenes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!has_key_constraints|| inventory.GetKey() == required_key)
            && collision.gameObject.name == "Player")
        {
            Debug.Log("pass");
            EventBus.Publish<StageStatus>(new StageStatus((current_level_index + 1) % total_scenes_count));
            StartCoroutine(load.GetComponent<levelLoder>().LoadLevel(current_level_index));
            GameObject completed_levels = GameObject.Find("CompletedLevels");
            if (completed_levels != null)
            {
                completed_levels.GetComponent<CompletedLevels>().level_completed[current_level_index] = true;
            }
        }
    }

    void CountTotalScenes()
    {
        total_scenes_count = 8;
        /*
        if (EditorBuildSettings.scenes != null){
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach(EditorBuildSettingsScene scene in scenes)
        {
            if(scene.enabled)
            {
                total_scenes_count++;
            }
        }
        }*/
    }
}
