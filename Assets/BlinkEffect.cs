using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Color curr_color;
    SpriteRenderer warning_tile;
    bool running = false;
    void Start()
    {
        warning_tile = GetComponent<SpriteRenderer>();
        running = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!running)
        {
            blink();
        }
    }

    IEnumerator blink()
    {
        Debug.Log("blinking");
        running = true;
        yield return new WaitForSeconds(1f);
        warning_tile.color = Color.red;
        yield return new WaitForSeconds(1f);
        warning_tile.color = Color.white;
        running = false;
    }
}
