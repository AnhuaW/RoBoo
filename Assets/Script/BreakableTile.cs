using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    public float break_speed_lower_bound = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float instant_speed = collision.relativeVelocity.magnitude;
            Debug.Log("Instant speed: " + instant_speed);
            if(instant_speed> break_speed_lower_bound)
            {
                Destroy(gameObject);
            }
        }

    }

}
