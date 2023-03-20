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

    public Rigidbody2D rb;
    public Inventory_tmp inventory;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = Inventory_tmp.instance;
    }


    void Update()
    {
        //extra condition for falling block damage
        if(rb.gravityScale == 1 && rb.velocity.y < 0)
        {
            is_falling = true;
        }

        // if reaches screen edge, stop moving (mainly prevent object from floating infinitely)
        if (is_floating)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewportPos.x < 0f || viewportPos.x > 1f ||
            viewportPos.y < 0.1f || viewportPos.y > 0.9f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        // detect ground underneath
        if (is_falling)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            Vector2 center = transform.position + new Vector3(0, -0.6f, 0);
            RaycastHit2D hit = Physics2D.Raycast(center, new Vector2(0, -1), 0.1f);

            if (hit.collider != null && hit.collider.gameObject.name != "Player" && !hit.collider.isTrigger)
            {
                is_falling = false;
            }
            
        }
        
    }


    public void OnGravityChange()
    {
        Debug.Log("OnGravityChange");
        if (is_floating)
        {
            ApplyGravity();
            return;
        }
        else
        {
            if (inventory.GetBubbleAmmo() > 0)
            {
                RemoveGravity();
            }
        }
    }


    public virtual void RemoveGravity()
    {
        is_floating = true;
        is_falling = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(rb.velocity.x, upward_speed);
        // initiate a bubble
        GameObject new_bubble = Instantiate(bubble_prefab, transform.position, Quaternion.identity);
        new_bubble.transform.SetParent(transform);
        // consume ammo
        inventory.ChangeBubbleAmmo(-1);

    }

    public void ApplyGravity()
    {
        is_floating = false;
        is_falling = true;
        Debug.Log("isfalling:" + is_falling);
        //rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
        //rb.velocity = Vector2.zero;
        // remove bubble
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform temp_child = transform.GetChild(i);
            if (temp_child.gameObject.name.Contains("Bubble"))
            {
                temp_child.parent = null;
                Destroy(temp_child.gameObject);
            }
        }

    }


    // bubble bursts when touching a collider
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (is_floating && !is_falling)
        {
            ApplyGravity();
        }
    }


}
