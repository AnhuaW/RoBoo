using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // press Esc to go back to menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            // clear inventory
            Inventory_tmp.instance.ClearAll();
        }
        // press R to load latest checkpoint
        if(Input.GetKeyDown(KeyCode.R))
        {
            EventBus.Publish<GameOver>(new GameOver());
        }
        // press T to restart current level
        if(Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<GameStatus>().has_checked = false;
            EventBus.Publish<GameOver>(new GameOver());
        }
    }
}
