using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// smash the player when falling down
public class Smash : MonoBehaviour
{
    LayerMask player_layer;

    public float detect_range = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player_layer = 1 << LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 left_edge = transform.position - new Vector3(0.3f, 0.4f, 0); 
        RaycastHit2D hit = Physics2D.Raycast(left_edge, new Vector2(0, -1), detect_range, player_layer);
        if(hit.collider!=null && hit.collider.gameObject.name == "Player")
        {
            Debug.Log("smash");
            EventBus.Publish<GameOver>(new GameOver());
        }
        Debug.DrawRay(left_edge, new Vector2(0, -1) * detect_range, Color.red);
    }
}
