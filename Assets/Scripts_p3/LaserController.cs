using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls laser's endpoints and color
public class LaserController : MonoBehaviour
{
    [SerializeField] Color color = new Color(191 / 255, 36 / 255, 0);
    Transform parent, start_pt, end_pt;
    [SerializeField] string death_message = "Killed by laser!";

    // raycast will detect objects in these layers to destroy
    LayerMask player_layer, shield_layer;
    LineRenderer line;

    void Start()
    {
        // get references to objects
        player_layer = 1 << LayerMask.NameToLayer("Player");
        shield_layer = 1 << LayerMask.NameToLayer("Shield");
        line = GetComponent<LineRenderer>();
        parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.gameObject.name == "StartPoint")
            {
                start_pt = child;
            }
            else if (child.gameObject.name == "EndPoint")
            {
                end_pt = child;
            }
        }
        // set line color
        line.SetColors(color, color);

        UpdateEndPoint();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEndPoint();
    }


    // update laser and raycast
    void UpdateEndPoint()
    {
        Vector2 start_pos = start_pt.localPosition;
        Vector2 end_pos = end_pt.localPosition;
        Vector2 direction = end_pos - start_pos;

        // redraw laser
        line.SetPosition(0, start_pos);
        line.SetPosition(1, end_pos);
        
        // detect and damage player or shield (whichever is hit first)
        Vector2 ray_start_pos = start_pt.position;
        LayerMask layers = player_layer | shield_layer;
        RaycastHit2D hit = Physics2D.Raycast(ray_start_pos, direction, direction.magnitude, layers);
        Debug.DrawRay(ray_start_pos, direction, Color.red);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Player")
            {
                EventBus.Publish<GameOver>(new GameOver(false, death_message));
            }
            else
            {
                Destroy(hit.collider.gameObject);
            }
        }

    }

}
