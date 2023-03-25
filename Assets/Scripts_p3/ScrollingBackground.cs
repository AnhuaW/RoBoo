using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollSpeed = 0.5f;
    GameObject player;
    Rigidbody2D rb;
    [SerializeField]
    private float moveX;
    [SerializeField]
    private float plyaerLastPos;
    private float direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        plyaerLastPos = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //moveX = player.GetComponent<ArrowKeyMovement>().playerDirection.x;
        moveX = player.transform.position.x - plyaerLastPos;

        if(moveX >= 0.055)
        {
            direction = 1;
        }

        else if (moveX <= -0.055)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }
        
        StartScroll(-direction * scrollSpeed);
        plyaerLastPos = player.transform.position.x;
    }

    void StartScroll(float speed)
    {
        rb.velocity = new Vector2(speed, 0);
    }
}
