using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    public float movement_speed = 4;
    public Vector2 playerDirection;
    public bool movement_frozen = false;
    public float jumpHeight = 1f;

    Rigidbody2D rb;

    private Vector2 current_input;
    private float horizontal_input;
    private float vertical_input;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        current_input = Vector2.zero;

        if (!movement_frozen)
        {
            current_input = GetInput();
        }

        rb.velocity = current_input * movement_speed;



        if (GetComponent<GravityControl>().enable_gravity)
        {
            rb.freezeRotation = true;
        }
        else
        {
            rb.freezeRotation = false;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !movement_frozen)
        {
            if (GetComponent<GravityControl>().enable_gravity)
            {
                Jump();
            }
        }
    }

    Vector2 GetInput()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        vertical_input = Input.GetAxisRaw("Vertical");
        if (GetComponent<GravityControl>().enable_gravity)
        {
            vertical_input = 0;
            movement_speed = 4;
        }

        else if (!GetComponent<GravityControl>().enable_gravity)
        {
            movement_speed = 2;
        }
        if(Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }

        if(Mathf.Abs(vertical_input) > 0.0f)
        {
            horizontal_input = 0.0f;
        }
        playerDirection = new Vector2(horizontal_input, vertical_input);
        return new Vector2(horizontal_input, vertical_input);
    }

    void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        Debug.Log("jump");
    }
}
