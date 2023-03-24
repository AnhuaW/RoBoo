using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeLifted : MonoBehaviour
{
    public GameObject left_block;
    public GameObject right_block;
    public float upSpeed = 4f;
    Rigidbody2D rb;
    bool lifting = false;
    bool can_be_lifted = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //updated lifting based on the status of the two blocks
        can_be_lifted = left_block.GetComponent<Floatable>().is_floating && right_block.GetComponent<Floatable>().is_floating;

        //Start Lifting if not yet lifted
        if (can_be_lifted && !lifting)
        {
            LiftPlatform();
        }

        if (!can_be_lifted)
        {
            DropPlatform();
        }

    }

    void LiftPlatform()
    {
        lifting = true;
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = new Vector2(0, upSpeed);
    }

    void DropPlatform()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;
    }
}
