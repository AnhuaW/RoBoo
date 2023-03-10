using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToNextLevel : MonoBehaviour
{
    public int current_level_index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            EventBus.Publish<StageStatus>(new StageStatus(current_level_index + 1));
        }
    }
}
