using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// an easy way to prevent bricks from getting stuck
// in waterflow and colliding with other objects
public class CheckBrickStuck : MonoBehaviour
{
    [SerializeField] float detect_range = 0.1f;

    Floatable floatable;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        floatable = GetComponent<Floatable>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();

        Vector2 left_edge = transform.position - new Vector3(0.51f, 0f, 0);
        RaycastHit2D hit = Physics2D.Raycast(left_edge, new Vector2(-1, 0), detect_range);
        Debug.DrawRay(left_edge, new Vector2(-1, 0) * detect_range, Color.red);
        if (hit.collider != null && !hit.collider.isTrigger)
        {
            
            if (rb.velocity.x < 0 && !floatable.is_floating)
            {
                Debug.Log("hey" + hit.collider.gameObject.name);
                transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            }
            
            


        }

        Vector2 right_edge = transform.position + new Vector3(0.48f, 0f, 0);
        hit = Physics2D.Raycast(left_edge, new Vector2(-1, 0), detect_range);
        Debug.DrawRay(right_edge, new Vector2(-1, 0) * detect_range, Color.red);
        if (hit.collider != null)
        {


        }

    }


    bool GroundCheck()
    {
        Vector2 center = transform.position + new Vector3(0, -0.51f, 0);
        RaycastHit2D hit = Physics2D.Raycast(center, new Vector2(0, -1), 0.1f);
        if (hit.collider != null)
        {
            Debug.Log("brick groundcheck:" + hit.collider.name);
            return true;
        }
        return false;
    }

}
