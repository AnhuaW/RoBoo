using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool enable_gravity = true;
    public bool gravity_enabled = true;
    public float current_gravity_scale = 10f;
    public float time_interval = 0.5f;
    public float rotation = 10f;
    Rigidbody2D rb;
    Inventory inventory;
    private float orig_height = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gravity_enabled)
        {//ensure the player does not rotate when not floating
            rb.SetRotation(0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!enable_gravity && inventory.get_energy_level() > 0)
            {
                orig_height = transform.position.y;
                enable_gravity = true;
            }

            else if (enable_gravity)
            {
                enable_gravity = false;
            }

            /*if (enable_gravity)
            {
                //inventory.decrease_energy(1);
                orig_height = transform.position.y;
            }*/
        }

        if(enable_gravity && !gravity_enabled)
        {
            StartCoroutine(EnableGravity());
        }

        if (enable_gravity)
        {
            rb.gravityScale = current_gravity_scale;
        }

        if(!enable_gravity && gravity_enabled)
        {
            StartCoroutine(DisableGravity());
        }
    }

    public IEnumerator EnableGravity()
    {
        //immediately turns it off
        if(GetComponent<FloatingEffect>() != null)
        {
            GetComponent<FloatingEffect>().enabled = false;
        }
        rb.gravityScale = current_gravity_scale;
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 50f;
        rb.SetRotation(0);
        gravity_enabled = true;
    }

    public IEnumerator DisableGravity()
    {
        //TODO
        if (GetComponent<FloatingEffect>() != null)
        {
            GetComponent<FloatingEffect>().enabled = true;
        }
        rb.gravityScale = -1;
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = 0;
        rb.SetRotation(0);
        gravity_enabled = false;
    }
}
