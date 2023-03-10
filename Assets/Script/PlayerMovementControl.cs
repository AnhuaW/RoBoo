using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    public float movement_speed = 4;
    public Vector2 playerDirection;
    public bool movement_frozen = false;
    public float jumpHeight = 2.5f;
    [Space]
    public float collisionRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    private Vector2 current_input;
    private float horizontal_input;
    private float vertical_input;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        current_input = Vector2.zero;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, collisionRadius, groundLayer);

        if (!movement_frozen)
        {
            current_input = GetInput();
        }

        if (GetComponent<GravityControl>().enable_gravity)
        {
            rb.freezeRotation = true;
        }
        else
        {
            rb.freezeRotation = false;
        }

        //JumpScale();
    }

    private void FixedUpdate()
    {
        rb.velocity = current_input * movement_speed;
        if (Input.GetKeyDown(KeyCode.Space) && !movement_frozen && isGrounded)
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
        rb.velocity = new Vector2(horizontal_input, jumpForce);
       
        Debug.Log(jumpForce);
        //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        Debug.Log("jump");
    }

    void JumpScale()
    {
        if (rb.velocity.y < 0 && !isGrounded)
        {
            rb.gravityScale = 20f;
        }
        else
        {
            rb.gravityScale = 10f;
        }
    }
}
