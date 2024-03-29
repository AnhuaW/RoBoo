using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTarget : MonoBehaviour
{
    public Camera cam;
    public float bubble_movement_speed = 2f; // tunable speed of bubble
    GameStatus gamestatus;

    void Start()
    {
        cam = Camera.main;
        gamestatus = GetComponent<GameStatus>();
    }

    void Update()
    {
        // left click mouse
        if (!gamestatus.gameover && Input.GetMouseButtonDown(0))
        {
            // get the position of mouse
            Vector3 mouse_position = cam.ScreenToWorldPoint(Input.mousePosition);
            mouse_position.z = 0;

            FloatObject(mouse_position);
        }
    }



    // directly let target object float
    // only works with selectable objects
    void FloatObject(Vector3 mouse_position)
    {
        // use Raycast to detect gameobject being clicked on
        RaycastHit2D hit = Physics2D.Raycast(mouse_position, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<Floatable>() != null)
        {
            // change target's gravity
            hit.collider.gameObject.GetComponent<Floatable>().OnGravityChange();
        }
    }

}
