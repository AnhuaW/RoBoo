using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float posX = 0f;
    public float posY = 3f;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + posY, transform.position.z);
    }
}
