using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// hurts player upon collision
public class DamagePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            EventBus.Publish<GameOver>(new GameOver());
        }
    }
}
