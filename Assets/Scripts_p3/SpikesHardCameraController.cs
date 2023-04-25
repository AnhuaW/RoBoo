using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// customized for last level
public class SpikesHardCameraController : MonoBehaviour
{
    Camera cam;
    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x <= 8f && player.transform.position.y < 5f && CountRemainingKeys() == 3)
        {
            cam.GetComponent<trackingPlayer>().offsetX = -5f;
        }
        else
        {
            cam.GetComponent<trackingPlayer>().offsetX = 4f;
        }
    }

    int CountRemainingKeys()
    {
        GameObject[] keys_remain = GameObject.FindGameObjectsWithTag("key");
        return keys_remain.Length;
    }
}
