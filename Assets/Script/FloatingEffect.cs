using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float time_interval = 1f;
    public float gravity = 0.5f;
    // Start is called before the first frame update
    Rigidbody2D rb;
    private bool float_ended = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (float_ended)
        {
            StartCoroutine(floating());
        }
    }

    IEnumerator floating()
    {
        float_ended = false;
        rb.gravityScale = -gravity;
        yield return new WaitForSeconds(time_interval);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = gravity;
        yield return new WaitForSeconds(time_interval);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        float_ended = true;
    }

    private void OnDisable()
    {
        rb.gravityScale = 1f;
    }
}
