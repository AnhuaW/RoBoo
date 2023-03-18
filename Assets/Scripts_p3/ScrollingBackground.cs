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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = player.GetComponent<ArrowKeyMovement>().playerDirection.x;
        Debug.Log(player.GetComponent<ArrowKeyMovement>().playerDirection.x);
        StartScroll(-moveX * scrollSpeed);
    }

    void StartScroll(float speed)
    {
        rb.velocity = new Vector2(speed, 0);
    }
}
