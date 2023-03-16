using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

// generate turbulent flow to give bubbles additional horizontal velocity
public class WaterFlow : MonoBehaviour
{
    public Vector2 half_flow_range = new Vector2(10, 0.5f);
    [SerializeField] float added_horizontal_speed = 2f;

    public List<GameObject> floatables_within_range;

    void Start()
    {

    }

    void Update()
    {
        // detect all floating objects in a certain range
        Vector2 center = new Vector2(transform.position.x + half_flow_range.x, transform.position.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(center, half_flow_range, 0f);
        //List<GameObject> floatables_within_range = new List<GameObject>();
        floatables_within_range = new List<GameObject>();
        foreach (Collider2D collider in colliders)
        {
            Floatable floatable = collider.gameObject.GetComponent<Floatable>();
            if (floatable != null && floatable.is_floating)
            {
                floatables_within_range.Add(collider.gameObject);
            }
        }


        // add velocity to the floatables
        foreach (GameObject floatable in floatables_within_range)
        {
            Rigidbody2D floatable_rb = floatable.GetComponent<Rigidbody2D>();
            if (floatable_rb != null)
            {
                floatable_rb.velocity = new Vector2(added_horizontal_speed, floatable_rb.velocity.y);
                Debug.Log(floatable_rb.velocity);
            }
        }
    }

    void OnDrawGizmos()
    {
        // debug drawing:
        Vector2 center = new Vector2(transform.position.x + half_flow_range.x, transform.position.y);
        Gizmos.DrawWireCube(center, half_flow_range * 2);
    }
}
