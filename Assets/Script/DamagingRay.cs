using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagingRay : MonoBehaviour
{
    public float rayLength = 0;
    public bool vertical_ray = true;

    [SerializeField] LineRenderer line;

    LayerMask player_layer;

    // Start is called before the first frame update
    void Start()
    {
        player_layer = 1 << LayerMask.NameToLayer("Player");
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical_ray)
        {
            CastVerticalRay();
        }
        else
        {
            CastHorizontallRay();
        }
    }



    void CastVerticalRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, 1), rayLength, player_layer);
        //Debug.DrawRay(transform.position, new Vector2(0, 1) * rayLength, Color.red);
        // draw ray
        //line.enabled = true;
        line.SetPosition(0, transform.position);
        Vector3 end_point = new Vector3(transform.position.x, transform.position.y + rayLength, transform.position.z);
        line.SetPosition(1, end_point);
        if (hit.collider != null)
        {

            // TODO: add game over scripts
            Debug.Log("damaged by ray, gameover");
            GameOver game_status = new GameOver();
            EventBus.Publish<GameOver>(game_status);
        }
    }

    void CastHorizontallRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0), rayLength, player_layer);
        // draw ray
        line.SetPosition(0, transform.position);
        Vector3 end_point = new Vector3(transform.position.x + rayLength, transform.position.y, transform.position.z);
        line.SetPosition(1, end_point);
        if (hit.collider != null)
        {
            Debug.Log("damaged by ray, gameover");
            GameOver game_status = new GameOver();
            EventBus.Publish<GameOver>(game_status);
        }
    }

}
