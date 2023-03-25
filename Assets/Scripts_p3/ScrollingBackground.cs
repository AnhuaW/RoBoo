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
    private float CamLastPos;
    private float direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        CamLastPos = Camera.main.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //moveX = player.GetComponent<ArrowKeyMovement>().playerDirection.x;
        moveX = Camera.main.transform.position.x - CamLastPos;

        if(moveX >= 0.05)
        {
            direction = 1;
        }

        else if (moveX <= -0.05)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }
        
        StartScroll(direction * scrollSpeed);
        CamLastPos = Camera.main.transform.position.x;
    }

    void StartScroll(float speed)
    {
        rb.velocity = new Vector2(speed, 0);
    }
}
