using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelProgression : MonoBehaviour
{
    // Start is called before the first frame update 
    //Scene scene;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            //make sure the player don't fall out and just die
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<GravityControl>().enable_gravity = false;
        }
    }
}
