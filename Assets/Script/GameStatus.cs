using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{

    public bool gameover = false;

    Subscription<GameOver> gameover_subscription;

    // Start is called before the first frame update
    void Start()
    {
        gameover_subscription = EventBus.Subscribe<GameOver>(_OnGameOver);
    }

    // Update is called once per frame
    void Update()
    {
        // restart the level
        if (gameover)
        {
            // reload scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // retrieve initial inventory state
            //Inventory_tmp.instance.RetrieveInitialState();
        }
    }

    void _OnGameOver(GameOver g)
    {
        gameover = true;
    }


    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameover_subscription);
    }

}


public class GameOver
{
    public GameOver() { }
}