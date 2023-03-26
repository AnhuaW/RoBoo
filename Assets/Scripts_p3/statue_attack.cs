using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statue_attack : MonoBehaviour
{
    //public Sprite stop_angle;
    //public Sprite moving_angle;
    GameObject player;
    Rigidbody2D player_rigid;
    Rigidbody2D rb;
    SpriteRenderer sp;
    SpriteRenderer player_sp;
    public float moving_speed = 0.5f;
    public bool isGrounded = true;
    public Vector3 oriPos;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player_rigid = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player_sp = player.GetComponent<SpriteRenderer>();
        Debug.Log(player_sp.flipX);
        oriPos = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if(player_sp.flipX == sp.flipX)
        {
            if(isGrounded)
            {
                isGrounded = false;
                animator.SetTrigger("notGrounded");
                Debug.Log("backtoOri");
                transform.position = oriPos;
            }
            GetComponent<BoxCollider2D>().isTrigger = true;
            rb.gravityScale = 0;
            //sp.sprite = moving_angle;
            
            Vector3 dir =  player.transform.position - transform.position;
            //Debug.Log(Mathf.Atan2(dir.y, dir.x));
            rb.velocity = new Vector2(Mathf.Sin(Mathf.Atan2(dir.x, dir.y)) * moving_speed,
                Mathf.Cos(Mathf.Atan2(dir.x, dir.y)) * moving_speed);
            oriPos = transform.position;
        }
        else
        {
            if(!isGrounded)
            {
                
                animator.SetTrigger("isGrounded");
            }
                
            //Debug.Log("stop");
            GetComponent<BoxCollider2D>().isTrigger = false;
            //sp.sprite = stop_angle;
            
            rb.velocity = Vector2.zero;
            rb.gravityScale = 8;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("bar"))
        {
            isGrounded = true;
        }
    }
}
