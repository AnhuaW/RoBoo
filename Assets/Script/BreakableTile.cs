using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    public float break_speed_lower_bound = 4f;
    public AudioClip destroy;
    public float destroy_sfx_volume;
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
        if (collision.gameObject.CompareTag("statue"))
        {
            StartCoroutine(breakTiles());
        }
        else if (rb != null)
        {
            Vector2 collision_velocity = collision.relativeVelocity;
            Debug.Log("breakable tile collision velocity: " + collision_velocity);
            float instant_speed = collision.relativeVelocity.magnitude;
            //Debug.Log("Instant speed: " + instant_speed);
            //if (collision_velocity.y <0 && instant_speed > break_speed_lower_bound)
            if (Mathf.Abs(collision_velocity.y) > break_speed_lower_bound)
            {
                Debug.Log("break");
            StartCoroutine(breakTiles());
            }
        }

    }

    IEnumerator breakTiles()
    {
        AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position, destroy_sfx_volume);
        yield return 0.5f;
        Destroy(gameObject);
    }

}
