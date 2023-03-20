using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

// generate turbulent flow to give bubbles additional horizontal velocity
public class WaterFlow : MonoBehaviour
{
    [SerializeField] float added_speed = 0f;
    [SerializeField] Vector3 flow_boxSize = Vector3.one;

    void Start()
    {

    }

    void Update()
    {
        AddHorizontalVelocity();
    }


    // detect floating objects in box, add velocity
    // require floatables to have Rigidbody2D
    void AddHorizontalVelocity()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, flow_boxSize, 0f);
        foreach (Collider2D collider in colliders)
        {
            //Debug.Log("overlapbox: " + collider.gameObject.name);
            Floatable floatable = collider.gameObject.GetComponent<Floatable>();
            if (floatable != null && floatable.is_floating)
            {
                Rigidbody2D floatable_rb = collider.gameObject.GetComponent<Rigidbody2D>();
                if (floatable_rb != null && floatable_rb.velocity.x == 0)
                {
                    Debug.Log("overlap rb: " + floatable.gameObject.name);
                    StartCoroutine(AddHorizontalVelocity(floatable_rb));
                }
            }
        }
    }
    // add horizontal speed for 1 sec, then reset velocity.x
    IEnumerator AddHorizontalVelocity(Rigidbody2D floatable_rb)
    {
        floatable_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        floatable_rb.velocity = new Vector2(added_speed, floatable_rb.velocity.y);
        yield return new WaitForSeconds(1);
        floatable_rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        floatable_rb.velocity = new Vector2(0, floatable_rb.velocity.y);
    }


    // draw debug box
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, flow_boxSize);
    }



    /*
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
    */
}
