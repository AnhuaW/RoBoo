using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{
    public float movement_speed = 4;
    public Vector2 playerDirection;
    public bool movement_frozen = false;

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
    }

    Vector2 GetInput()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        vertical_input = Input.GetAxisRaw("Vertical");
        if(Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }

        if(Mathf.Abs(vertical_input) > 0.0f)
        {
            horizontal_input = 0.0f;
        }

        return new Vector2(horizontal_input, vertical_input);
    }
}
