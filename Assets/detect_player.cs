using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect_player : MonoBehaviour
{
    Vector3 oriPos;
    GameObject player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.position;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x));
        if (Mathf.Abs(player.transform.position.x-transform.position.x) < 0.5f
            && transform.position == oriPos)
        {
            rb.gravityScale = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventBus.Publish<GameOver>(new GameOver());
        }
    }
}
