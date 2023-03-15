using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    float current_health = 3f;
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
        if (collision.gameObject.CompareTag("bullet"))
        {
            current_health -= 0.5f;
            StartCoroutine(KnockByEnemy(collision.gameObject));
            
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
            StartCoroutine(KnockByEnemy(collision.gameObject));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            current_health -= 0.25f;
            StartCoroutine(KnockByEnemy(collision.gameObject));
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator KnockByEnemy(GameObject other)
    {
        Vector3 dir = Vector3.Normalize(transform.position - other.transform.position);
        int count = 0;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            
            if (count % 2f == 0)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            count += 1;
            yield return new WaitForSeconds(Time.deltaTime);
            //yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return null;
    }
}
