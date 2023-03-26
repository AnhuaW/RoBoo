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
    //public bool is_moving;
    public float orig_posX;
    public float orig_posY;
    public float target_posX;
    public float target_posY;
    int dir = 1;
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
        CalcTarget(dir);
        SetVelocity(dir);
        Debug.Log(rb.velocity);    
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.y == target_posY);

        if(transform.position.x >= target_posX && transform.position.y >= target_posY )
        {
            if(dirBetween() == 1)
            {
                Debug.Log("change");
                dir = dir * (-1);
                CalcTarget(dir);
                SetVelocity(dir);
                orig_posY = target_posY;
                orig_posX = target_posX;
            }
            
        }
        else if(transform.position.x <= target_posX && transform.position.y <= target_posY)
        {
            if(dirBetween() == -1)
            {
                Debug.Log("change");
                dir = dir * (-1);
                CalcTarget(dir);
                SetVelocity(dir);
                orig_posY = target_posY;
                orig_posX = target_posX;
            }

            
            
        }

    }

    void CalcTarget(int dir)
    {
        target_posX += dir * horizontal_direction * move_range;
        target_posY += dir * vertical_direction * move_range;

    }

    void SetVelocity(int dir)
    {
        rb.velocity = new Vector2(dir * horizontal_direction * move_speed, dir * vertical_direction * move_speed);
    }

    int dirBetween()
    {
        if (orig_posX > target_posX || orig_posY > target_posY)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
