using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToNextLevel : MonoBehaviour
{
    public int current_level_index = 0;
    public int total_scenes_count = 6;
    GameObject load;

    // Start is called before the first frame update
    void Start()
    {
        load = GameObject.Find("levelLoader");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //EventBus.Publish<StageStatus>(new StageStatus((current_level_index + 1) % total_scenes_count));
            StartCoroutine(load.GetComponent<levelLoder>().LoadLevel(current_level_index));
            GameObject completed_levels = GameObject.Find("CompletedLevels");
            if (completed_levels != null)
            {
                completed_levels.GetComponent<CompletedLevels>().level_completed[current_level_index] = true;
            }
        }
    }
}
