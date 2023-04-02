using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintControl : MonoBehaviour
{
    public SpriteRenderer hint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hint.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hint.enabled = false;
        }
    }

    // Update is called once per frame

}
