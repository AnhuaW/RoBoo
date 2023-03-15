using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    CameraControl cameraMove;
    Vector2 direction;
    void Start()
    {
        player = GameObject.Find("Player");
        cameraMove = Camera.main.GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.GetComponent<PlayerMovementControl>().playerDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("playerTriggered");
            GameObject player = other.gameObject;
            //player going right
            if (direction.x > 0)
            {
                StartCoroutine(cameraMove.moveRight());
            }

            else if (direction.x < 0)
            {
                StartCoroutine(cameraMove.moveLeft());
            }


            else if (direction.y < 0)
            {
                StartCoroutine(cameraMove.moveDown());
            }


            else if (direction.y > 0)
            {
                StartCoroutine(cameraMove.moveUp());
            }

        }
    }
}
