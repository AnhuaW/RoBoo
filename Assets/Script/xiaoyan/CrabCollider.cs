using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("damaged by fish, gameover");
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("damaged by ray, gameover");
            GameOver game_status = new GameOver();
            EventBus.Publish<GameOver>(game_status);
        }
    }
}
