using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintControl : MonoBehaviour
{
    public SpriteRenderer hint;
    public string tagName = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagName))
        {
            hint.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagName))
        {
            hint.enabled = false;
        }
    }

    // Update is called once per frame

}
