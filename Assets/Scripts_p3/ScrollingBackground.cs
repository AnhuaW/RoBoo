using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollSpeed = 0.5f;
    Transform cam;
    Rigidbody2D rb;
    [SerializeField]
    private float moveX;
    [SerializeField]
    private float LastPos;
    private float direction;

    void Start()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody2D>();
        LastPos = cam.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //moveX = player.GetComponent<ArrowKeyMovement>().playerDirection.x;
        moveX = cam.transform.position.x - LastPos;

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
        LastPos = cam.position.x;
    }

    void StartScroll(float speed)
    {
        rb.velocity = new Vector2(speed, 0);
    }
}
