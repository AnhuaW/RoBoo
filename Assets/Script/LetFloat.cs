using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LetFloat : MonoBehaviour
{
    //public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Vector3.Distance(target.transform.position, transform.position) < 0.1f)
        {
            // bind bubble and target
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.SetParent(target.transform, false);

            // let target float
            Rigidbody2D target_rb = target.GetComponent<Rigidbody2D>();
            target_rb.gravityScale = 0f;
            target_rb.velocity = new Vector2(target_rb.velocity.x, 2f);

        }
        */
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CursorSelect>() != null)
        {
            // bind bubble and target
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.SetParent(collision.transform, false);

            // let target float
            Rigidbody2D target_rb = collision.gameObject.GetComponent<Rigidbody2D>();
            target_rb.gravityScale = 0f;
            target_rb.velocity = new Vector2(target_rb.velocity.x, 2f);
        }
    }


}

