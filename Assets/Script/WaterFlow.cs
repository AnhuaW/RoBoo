using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

// generate turbulent flow to give bubbles additional horizontal velocity
public class WaterFlow : MonoBehaviour
{
    [SerializeField] float added_horizontal_speed = 2f;

    void Start()
    {

    }

    void Update()
    {

    }


    // detect all floating objects in the range
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Floatable>() != null && collision.gameObject.GetComponent<Floatable>().is_floating)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (collision.gameObject.name == "ball")
                {
                    rb.velocity = new Vector2(added_horizontal_speed, rb.velocity.y);
                }
                else
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = new Vector2(added_horizontal_speed, rb.velocity.y);
                }
                
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Floatable>() != null)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if(collision.gameObject.name == "ball")
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Floatable>() != null && collision.gameObject.GetComponent<Floatable>().is_floating)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && rb.velocity.x == 0)
            {
                if (collision.gameObject.name == "ball")
                {
                    rb.velocity = new Vector2(added_horizontal_speed, rb.velocity.y);
                }
                else
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = new Vector2(added_horizontal_speed, rb.velocity.y);
                }
            }
        }
    }
}
