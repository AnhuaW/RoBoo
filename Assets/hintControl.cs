using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Vector3 curr_view_pos;
    SpriteRenderer[] sprites;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curr_view_pos = Camera.main.WorldToViewportPoint(transform.position);
        if (curr_view_pos.x >= 0.1f && curr_view_pos.x <= 0.9f )
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.enabled = true;
            }
        }

        else
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.enabled = false;
            }
        }
    }
}
