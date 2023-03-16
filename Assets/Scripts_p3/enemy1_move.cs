using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1_move : MonoBehaviour
{
    public float movement_speed = 0.5f;
    public Vector2 oriPos;
    public Vector2 current_input;
    List<bool> raycast_list = new List<bool>();
    //public float stopTime = 2f;
    RaycastHit2D isUpHit;
    RaycastHit2D isRightHit;
    RaycastHit2D isLeftHit;
    RaycastHit2D isDownHit;
    public float durationTime = 10f;
    public float lastTime = 0;
    public bool first = true;
    private int layerMask = 1 << 3;
    
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        rb = GetComponent<Rigidbody2D>();
        oriPos = transform.position;
        current_input = new Vector2(1, 0);
        /*
        System.Random rnd = new System.Random();
        int ranDir = rnd.Next(1, 5);
        current_input = SetDirection(ranDir);
        rb.velocity = current_input * movement_speed;

        Debug.Log(rb.velocity);
        */
    }
    
    // Update is called once per frame
    void Update()
    {
        if(current_input.x == 1 && isRightHit)
        {
            System.Random rnd = new System.Random();
            int ranDir = rnd.Next(1, 5);
            current_input = SetDirection(ranDir);
        }
        else if(current_input.x == -1 && isLeftHit)
        {
            System.Random rnd = new System.Random();
            int ranDir = rnd.Next(1, 5);
            current_input = SetDirection(ranDir);
        }
        else if(current_input.y == 1 && isUpHit)
        {
            System.Random rnd = new System.Random();
            int ranDir = rnd.Next(1, 5);
            current_input = SetDirection(ranDir);
        }
        else if(current_input.y == -1 && isDownHit)
        {
            System.Random rnd = new System.Random();
            int ranDir = rnd.Next(1, 5);
            current_input = SetDirection(ranDir);
        }
        else if(current_input.x < -1 || current_input.x > 1 || current_input.y <-1 || current_input.y > 1)
        {
            System.Random rnd = new System.Random();
            int ranDir = rnd.Next(1, 5);
            current_input = SetDirection(ranDir);
        }
        rb.velocity = current_input * movement_speed;
        //Debug.Log("starttomove");
        /*
        if (Mathf.Abs(oriPos.x - transform.position.x) >= 1f || Mathf.Abs(oriPos.y - transform.position.y) >= 1f)
        {
            System.Random rnd = new System.Random();
            int ranDir = rnd.Next(1, 5);
            current_input = SetDirection(ranDir);
            rb.velocity = current_input * movement_speed;
            oriPos = transform.position;

        }
        else if (Mathf.Abs(oriPos.x - transform.position.x) == 0f && Mathf.Abs(oriPos.y - transform.position.y) == 0f)
        {
            System.Random rnd = new System.Random();
            int ranDir = rnd.Next(1, 5);
            current_input = SetDirection(ranDir);
            rb.velocity = current_input * movement_speed;
            oriPos = transform.position;
        }
        */
        if(current_input.x == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (current_input.x == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    private void FixedUpdate()
    {
        isUpHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 0.5f, layerMask);
        isRightHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.5f, layerMask);
        isLeftHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.5f, layerMask);
        isDownHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.5f, layerMask);
    }

    Vector2 SetDirection(int ranDir)
    {
        List<RaycastHit2D> directions = new List<RaycastHit2D> { isLeftHit, isRightHit, isDownHit, isUpHit };
        int count = 4;
        if (directions[0] && directions[1] && directions[2] && directions[3]) { Debug.Log("stuck"); }
       
        for (int i = 0; i < 4; i++)
        {
            if (directions[i])
            {
                count -= 1;
                raycast_list.Add(true);
            }
            else
            {
                raycast_list.Add(false);
            }
        }
        int ran = ranDir % count;
        int index = 0;
        for (int i = 0; i < 4; i++)
        {
            if (!directions[i])
            {

                if (ran == 0)
                {
                    index = i + 1;
                    break;
                }
                ran -= 1;
            }
        }
        Debug.Log(index + "index");
        if (index <= 2)
        {
            return new Vector2(index * 2 - 3, 0);
        }
        else
        {
            return new Vector2(0, index * 2 - 7);
        }
    }

  
    /*
    private void OnTriggerEnter(Collider other)
    {
        GameObject object_collided_with = other.gameObject;

        if (object_collided_with.tag == "wall")
        {
            changeDir();
        }
    }
    */

    /*
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("collision");
        System.Random rnd = new System.Random();
        int ranDir = rnd.Next(1, 5);
        current_input = SetDirection(ranDir);

        if (current_input.x != 0)
        {
            transform.position =
                new Vector3(transform.position.x, RoundGrid(transform.position.y), 0);
        }
        if (current_input.y != 0)
        {
            transform.position =
                new Vector3(RoundGrid(transform.position.x), transform.position.y, 0);
        }
        rb.velocity = current_input * movement_speed;
        oriPos = transform.position;
    }
    
    private void changeDir()
    {
        //Debug.Log("collision");
        System.Random rnd = new System.Random();
        int ranDir = rnd.Next(1, 5);
        current_input = SetDirection(ranDir);
       
        if (current_input.x != 0)
        {
            transform.position =
                new Vector2(transform.position.x, RoundGrid(transform.position.y));
        }
        if (current_input.y != 0)
        {
            transform.position =
                new Vector2(RoundGrid(transform.position.x), transform.position.y);
        }
        rb.velocity = current_input * movement_speed;
        oriPos = transform.position;
        
    }*/
}
