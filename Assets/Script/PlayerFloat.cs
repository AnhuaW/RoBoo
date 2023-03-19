using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerFloat : Floatable
{
    public float bubble_duration_sec = 4f;
    GameObject bubble = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = Inventory_tmp.instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<ArrowKeyMovement>().isGrounded)
        {
            is_falling = false;
        }

    }

    public override void RemoveGravity()
    {
        Debug.Log("start: RemovePlayerGravity()");
        is_floating = true;
        is_falling = false;
        rb.gravityScale = 0;
        //rb.AddForce(Vector2.up, ForceMode2D.Impulse);
        StartCoroutine(Float());

        // initiate a bubble
        bubble = Instantiate(bubble_prefab, transform.position, Quaternion.identity);
        bubble.transform.SetParent(transform);
        // consume ammo
        inventory.ChangeBubbleAmmo(-1);
        // start a timer
        StartCoroutine(TimeLimit());
    }

    IEnumerator Float()
    {
        //rb.AddForce(Vector2.up, ForceMode2D.Impulse);
        rb.velocity = Vector2.up;
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 1f);
        /*
        GetComponent<ArrowKeyMovement>().player_control = false;
        
        float duration_sec = 1f;
        Vector3 initial_pos = transform.position;
        Vector3 dest_pos = transform.position + new Vector3(0, 1f, 0);
        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            // Recalculate the progress variable every frame. Use it to determine
            // new position on line from "initial_pos" to "dest_pos"
            progress = (Time.time - initial_time) / duration_sec;
            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            transform.position = new_position;

            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        transform.position = dest_pos;
        

        GetComponent<ArrowKeyMovement>().player_control = true;
        Debug.Log("Player control: " + GetComponent<ArrowKeyMovement>().player_control);
        */
    }


    IEnumerator TimeLimit()
    {
        // no blinking:
        /*
        yield return new WaitForSeconds(bubble_duration_sec);
        if (is_floating)
        {
            ApplyGravity();
        }
        */

        // with blinking effects:
        float blink_sec = 1f;
        yield return new WaitForSeconds(bubble_duration_sec - blink_sec);

        float initial_time = Time.time;
        // bubble blinks to remind player of time limit
        while (is_floating && bubble != null && (Time.time - initial_time < blink_sec))
        {
            bubble.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            bubble.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        if (is_floating)
        {
            ApplyGravity();
        }
    }

}
