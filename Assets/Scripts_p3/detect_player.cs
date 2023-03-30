using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detect_player : MonoBehaviour
{
    Vector3 oriPos;
    GameObject player;
    Rigidbody2D rb;
    private int player_layer;
    RaycastHit2D hit;
    Vector2 ballDir;
    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.position;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        player_layer = 1 << LayerMask.NameToLayer("Player");

    }

    // Update is called once per frame
    void Update()
    {
        ballDir = (rb.velocity).normalized;
        //Debug.Log(Mathf.Abs(player.transform.position.x - transform.position.x));
        if (Mathf.Abs(player.transform.position.x-transform.position.x) < 0.5f
            && transform.position == oriPos)
        {
            rb.gravityScale = 2;
        }
        //hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), detect_range, player_layer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventBus.Publish<GameOver>(new GameOver(false));
        }
    }
}
