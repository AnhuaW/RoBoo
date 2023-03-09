using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

// manipulate gameobject's gravity field
public class Floatable : MonoBehaviour
{
    public bool is_floating = false;
    public float upward_speed = 2f;
    public GameObject bubble_prefab;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
    }

    public void OnGravityChange()
    {
        if (is_floating)
        {
            ApplyGravity();
        }
        else
        {
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

    }

    void ApplyGravity()
    {
        is_floating = false;
        rb.gravityScale = 1;
        //rb.velocity = Vector2.zero;
        // remove bubble
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform temp_child = transform.GetChild(i);
            temp_child.parent = null;
            Destroy(temp_child.gameObject);
            Debug.Log("destroyed");
        }

    }

}
