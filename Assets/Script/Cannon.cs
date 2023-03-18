using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cannons only shoot toward designated directions (horizontal)
// (don't automatically track the player)
public class Cannon : MonoBehaviour
{
    public GameObject cannonball_prefab;
    public float cannonball_speed = 4f;
    public float shoot_period_sec = 2f;
    [SerializeField] Vector2 shoot_direction = Vector2.left;

    float last_shoot_time;
    Vector3 cannonball_launch_pos;


    // Start is called before the first frame update
    void Start()
    {
        last_shoot_time = Time.time;
        cannonball_launch_pos = transform.position + new Vector3(shoot_direction.x * 0.5f, 0.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - last_shoot_time >= shoot_period_sec)
        {
            // refresh the time of last shot
            last_shoot_time = Time.time;
            // launch cannonball
            GameObject cannonball = Instantiate(cannonball_prefab, cannonball_launch_pos, Quaternion.identity);
            Rigidbody2D cannonball_rb = cannonball.GetComponent<Rigidbody2D>();
            cannonball_rb.velocity = shoot_direction * cannonball_speed;
        }
    }

    // player dies when touching the cannon
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            EventBus.Publish<GameOver>(new GameOver());
        }
    }
}
