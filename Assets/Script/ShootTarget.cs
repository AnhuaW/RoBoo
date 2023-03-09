using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTarget : MonoBehaviour
{
    public Camera cam;
    public GameObject bubble_prefab;
    public float bubble_movement_speed = 2f; // tunable speed of bubble


    void Start()
    {
        
    }

    void Update()
    {
        // left click mouse
        if (Input.GetMouseButtonDown(0))
        {
            // get the position of mouse
            Vector3 mouse_position = cam.ScreenToWorldPoint(Input.mousePosition);
            mouse_position.z = 0;

            FloatObject(mouse_position);
        }
    }


    void LaunchBubble(Vector3 mouse_position)
    {
        GameObject new_bubble = Instantiate(bubble_prefab, transform.position, Quaternion.identity);
        Vector2 bubble_direction = mouse_position - transform.position;
        new_bubble.GetComponent<Rigidbody2D>().velocity = bubble_direction.normalized * bubble_movement_speed;
    }


    // directly let target object float
    // only works with selectable objects
    void FloatObject(Vector3 mouse_position)
    {
        // use Raycast to detect gameobject being clicked on
        RaycastHit2D hit = Physics2D.Raycast(mouse_position, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<CursorSelect>() != null)
        {
            GameObject new_bubble = Instantiate(bubble_prefab, hit.collider.gameObject.transform.position, Quaternion.identity);
            new_bubble.transform.SetParent(hit.collider.gameObject.transform);
            // TODO: replace following code with proper gravity controlling script
            Rigidbody2D rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 2f);
        }
    }

}
