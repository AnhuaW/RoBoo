using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontal_direction = 1;
    public float vertical_direction = 0;
    public float move_speed = 4f;
    public float move_range = 5f;
    public bool is_moving;
    float orig_posX;
    float orig_posY;
    float target_posX;
    float target_posY;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //get initial pos
        orig_posX = transform.position.x;
        orig_posY = transform.position.y;
        //set target to orig
        target_posX = orig_posX;
        target_posY = orig_posY;
        //calculate target based on desired moving direction
        CalcTarget();
        Debug.Log(target_posX);    
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal_direction != 0)
        {
            //Move the platform horizontally
            if(Mathf.Abs(target_posX - transform.position.x) <= move_range)
            {
                Debug.Log(transform.position.x);
                rb.velocity = new Vector2(horizontal_direction * move_speed, 0);
            }

        }

        if (vertical_direction != 0)
        {
            //Move the platform vertically
            if(Mathf.Abs(target_posY - transform.position.y) <= move_range)
            {
                rb.velocity = new Vector2(0, vertical_direction * move_speed);
            }
        }
    }

    void CalcTarget()
    {
        if (horizontal_direction != 0)
        {
            target_posX = orig_posX + horizontal_direction * move_range;
            target_posY = transform.position.y;
        }

        if (vertical_direction != 0)
        {
            target_posY = orig_posY + vertical_direction * move_range;
            target_posX = transform.position.x;
        }
    }

}
