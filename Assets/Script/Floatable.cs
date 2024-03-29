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

        // prevent pushable brick after floating
        if (gameObject.name.Contains("Brick"))
        {
            if (rb.velocity.x == 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            }
        }


        //extra condition for falling block damage
        if (rb.gravityScale == 1 && rb.velocity.y < 0)
        {
            is_falling = true;
        }

        // (disabled) if reaches screen edge, stop moving (mainly prevent object from floating infinitely)
        /*
        if (is_floating)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewportPos.x < 0f || viewportPos.x > 1f ||
            viewportPos.y < 0.1f || viewportPos.y > 0.9f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        */

        // detect ground underneath
        if (is_falling)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            Vector2 center = transform.position + new Vector3(0, -0.6f, 0);
            RaycastHit2D hit = Physics2D.Raycast(center, new Vector2(0, -1), 0.1f);
            //Debug.DrawRay(transform.position+new Vector3(0, -0.3f, 0), Vector2.down * 0.1f, Color.red);
            if (hit.collider != null && hit.collider.gameObject.name != "Player" && !hit.collider.isTrigger && hit.collider != GetComponent<Collider2D>())
            {

                is_falling = false;
                Debug.Log("flo" + hit.collider.name);
            }

            Vector2 left_edge = transform.position - new Vector3(0.48f, 0.5f, 0);
            hit = Physics2D.Raycast(left_edge, new Vector2(0, -1), 0.1f);
            if (hit.collider != null && hit.collider.gameObject.name != "Player" && !hit.collider.isTrigger && hit.collider != GetComponent<Collider2D>())
            {

                is_falling = false;
                Debug.Log("flo" + hit.collider.name);
            }

            Vector2 right_edge = transform.position + new Vector3(0.48f, -0.5f, 0);
            hit = Physics2D.Raycast(right_edge, new Vector2(0, -1), 0.1f);
            if (hit.collider != null && hit.collider.gameObject.name != "Player" && !hit.collider.isTrigger && hit.collider != GetComponent<Collider2D>())
            {

                is_falling = false;
                Debug.Log("flo" + hit.collider.name);
            }

        }

        // if colliding with tilemap or gameobjects
        // while being affected by waterflow
        if (rb.velocity.x != 0 && rb.velocity.y == 0 && !this.CompareTag("ball") && !is_falling)
        {
            ApplyGravity();

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
        if (this.gameObject.CompareTag("ball"))
        {
            rb.gravityScale = 3f;
        }
        else
        {
            rb.gravityScale = 1;
        }

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
        if(this.CompareTag("ball") && !is_floating && is_falling) {
            is_falling = false;
        }

    }

}
