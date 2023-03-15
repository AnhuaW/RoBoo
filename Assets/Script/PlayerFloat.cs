using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerFloat : Floatable
{


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = Inventory_tmp.instance;
    }

    // Update is called once per frame
    void Update()
    {



    }

    public override void RemoveGravity()
    {
        is_floating = true;
        rb.gravityScale = 0;
        StartCoroutine(Float());

        // initiate a bubble
        GameObject new_bubble = Instantiate(bubble_prefab, transform.position, Quaternion.identity);
        new_bubble.transform.SetParent(transform);
        // consume ammo
        inventory.ChangeBubbleAmmo(-1);
    }

    IEnumerator Float()
    {

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
        Debug.Log(GetComponent<ArrowKeyMovement>().player_control);
    }
}
