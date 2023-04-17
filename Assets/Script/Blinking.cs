using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    // Start is called before the first frame update
    bool running = false;
    SpriteRenderer spriteRend;
    public float interval = 0.8f;
    Color orig_color;
    public Color changing_color = Color.grey;
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        orig_color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            StartCoroutine(blink());
        }
    }

    IEnumerator blink()
    {
        running = true;
        spriteRend.color = orig_color;
        yield return new WaitForSeconds(interval);
        spriteRend.color = changing_color;
        yield return new WaitForSeconds(interval);
        running = false;
    }
}
