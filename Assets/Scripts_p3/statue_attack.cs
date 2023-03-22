using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statue_attack : MonoBehaviour
{
    public Sprite stop_angle;
    public Sprite moving_angle;
    GameObject player;
    Rigidbody2D player_rigid;
    Rigidbody2D rb;
    SpriteRenderer sp;
    SpriteRenderer player_sp;
    public float moving_speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player_rigid = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        
        player_sp = player.GetComponent<SpriteRenderer>();
        Debug.Log(player_sp.flipX);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if(player_sp.flipX == sp.flipX)
        {
            sp.sprite = moving_angle;
            Vector3 dir =  player.transform.position - transform.position;
            //Debug.Log(Mathf.Atan2(dir.y, dir.x));
            rb.velocity = new Vector2(Mathf.Sin(Mathf.Atan2(dir.x, dir.y)) * moving_speed,
                Mathf.Cos(Mathf.Atan2(dir.x, dir.y)) * moving_speed);
        }
        else
        {
            //Debug.Log("stop");
            sp.sprite = stop_angle;
            rb.velocity = Vector2.zero;
        }
        updateDir();
    }

    void updateDir()
    {
        if (player.transform.position.x > transform.position.x)
        {
            sp.flipX = false;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            sp.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EventBus.Publish<GameOver>(new GameOver());
        }
    }
}