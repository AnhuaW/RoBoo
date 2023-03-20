using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    public float break_speed_lower_bound = 4f;
    public AudioClip destroy;
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
        if (GetComponent<SpriteRenderer>().color != Color.white)
        {
            if (rb != null && collision.gameObject.CompareTag("ball"))
            {
                float instant_speed = collision.relativeVelocity.magnitude;
                Debug.Log("Instant speed: " + instant_speed);
                if (instant_speed > break_speed_lower_bound)
                {
                    Debug.Log("break");
                    StartCoroutine(breakTiles());
                }
            }
        }
        else
        {
            if (rb != null)
            {
                float instant_speed = collision.relativeVelocity.magnitude;
                Debug.Log("Instant speed: " + instant_speed);
                if (instant_speed > break_speed_lower_bound)
                {
                    Debug.Log("break");
                    StartCoroutine(breakTiles());
                }
            }
        }
        

    }

    IEnumerator breakTiles()
    {
        AudioSource.PlayClipAtPoint(destroy, Camera.main.transform.position);
        yield return 0.5f;
        Destroy(gameObject);
    }

}
