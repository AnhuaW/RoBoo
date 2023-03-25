using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontal_direction = 1;
    public float vertical_direction = 0;
    public float speed = 4f;
    public float movaing_range = 5f;
    public bool is_moving;
    float orig_posX;
    float orig_posY;
    float curr_posX;
    float curr_posY;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orig_posX = transform.position.x;
        orig_posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        curr_posX = transform.position.x;
        curr_posY = transform.position.y;
    }

    IEnumerator MovePlatformLR()
    {
        is_moving = true;
        yield return null;

    }

    IEnumerator MovePlatformUD()
    {
        is_moving = true;
        yield return null;
    }


    


}
