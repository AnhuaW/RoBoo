using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{
    public float ground_movement_speed = 3f;
    public float float_movement_speed = 2f;
    public float jump_force = 2f;
    public float collisionRadius = 0.2f;
    public Vector2 playerDirection;
    public bool player_control = true;

    Rigidbody2D rb;

    public bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 left_edge = transform.position - new Vector3(0.2f, 0.25f, 0);
        Vector2 right_edge = transform.position + new Vector3(0.2f, -0.25f, 0);
        Debug.DrawRay(left_edge, Vector2.down*0.1f, Color.red);
        Debug.DrawRay(right_edge, Vector2.down * 0.1f, Color.red);
        if (Physics2D.Raycast(left_edge, Vector2.down, 0.1f) || Physics2D.Raycast(right_edge, Vector2.down, 0.1f))
        {

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        if (!GetComponent<PlayerFloat>().is_floating)
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
        if (player_control)
        {
            Vector3 current_input = GetInput();
            float speed = (GetComponent<PlayerFloat>().is_floating ? float_movement_speed : ground_movement_speed);
            transform.position += current_input * speed * Time.fixedDeltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && !GetComponent<PlayerFloat>().is_floating && isGrounded)
            {
                Jump();
            }
        }

    }
    Vector3 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        // can't control vertical movement when not floating
        if (!GetComponent<Floatable>().is_floating)
        {
            vertical_input = 0;
        }
        else
        {
            // can't move in diagonal direction
            if (horizontal_input != 0)
            {
                vertical_input = 0.0f;
            }
            if (vertical_input != 0)
            {
                horizontal_input = 0.0f;
            }
        }

        playerDirection = new Vector2(horizontal_input, vertical_input);
        return new Vector3(horizontal_input, vertical_input, 0);
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        Debug.Log("jump");
    }


}