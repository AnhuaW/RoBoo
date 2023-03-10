using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

// manipulate gameobject's gravity field
public class Floatable : MonoBehaviour
{
    public bool is_floating = false; // whether there is gravity applied
    public bool is_falling = false;
    public float upward_speed = 2f;
    public GameObject bubble_prefab;

    Rigidbody2D rb;
    Inventory_tmp inventory;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = Inventory_tmp.instance;
    }


    void Update()
    {
        // if reaches screen edge, stop moving (mainly prevent object from floating infinitely)
        if (is_floating)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewportPos.x < 0.1f || viewportPos.x > 0.9f ||
            viewportPos.y < 0.1f || viewportPos.y > 0.9f)
            {
                rb.velocity = Vector2.zero;
            }
        }
        
    }

    /*
    private void FixedUpdate()
    {
        // simulate gravity
        if (is_falling)
        {
            Vector2 delta_pos = Physics2D.gravity * Time.fixedDeltaTime;
            Vector3 new_pos = transform.position + new Vector3(delta_pos.x, delta_pos.y, 0);
            transform.position = new_pos;
        }
    }
    */

    public void OnGravityChange()
    {
        if (is_floating)
        {
            ApplyGravity();
        }
        else
        {
            if (inventory.GetBubbleAmmo() > 0)
                RemoveGravity();
        }
    }


    void RemoveGravity()
    {
        is_floating = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, upward_speed);
        // initiate a bubble
        GameObject new_bubble = Instantiate(bubble_prefab, transform.position, Quaternion.identity);
        new_bubble.transform.SetParent(transform);
        // consume ammo
        inventory.ChangeBubbleAmmo(-1);

    }

    void ApplyGravity()
    {
        is_floating = false;
        is_falling = true;
        //rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        //rb.velocity = Vector2.zero;
        // remove bubble
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform temp_child = transform.GetChild(i);
            temp_child.parent = null;
            Destroy(temp_child.gameObject);
        }

    }


    // bubble bursts when touching a collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (is_floating && !is_falling)
        {
            ApplyGravity();
        }
        else if (is_falling)
        {
            is_falling = false;
        }
    }


}
