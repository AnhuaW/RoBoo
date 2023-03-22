using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ArrowKeyMovement : MonoBehaviour
{
    public float ground_movement_speed = 3f;
    public float float_movement_speed = 2f;
    public float jump_force = 2f;
    public float collisionRadius = 0.2f;
    public Vector2 playerDirection;
    public bool player_control = true;

    Rigidbody2D rb;

    public bool isGrounded = false;
    string sceneName;
    [SerializeField] GameObject Prompt;

    bool displayed = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        Debug.Log("SceneName: this is specific to scene name! for p3_guide_merge");
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 left_edge = transform.position - new Vector3(0.3f, 0.35f, 0);
        Vector2 right_edge = transform.position + new Vector3(0.3f, -0.35f, 0);
        Vector2 center = transform.position + new Vector3(0, -0.45f, 0);
        Debug.DrawRay(left_edge, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(right_edge, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(center, Vector2.down * 0.1f, Color.red);
        RaycastHit2D hit_left = Physics2D.Raycast(left_edge, Vector2.down, 0.1f);
        RaycastHit2D hit_right = Physics2D.Raycast(right_edge, Vector2.down, 0.1f);
        RaycastHit2D hit_center = Physics2D.Raycast(center, Vector2.down, 0.1f);
        /*
        if (hit_left)
        {
            Debug.Log("player left ray: " + hit_left.collider.gameObject.name);
        }
        if (hit_right)
        {
            Debug.Log("player right ray: " + hit_right.collider.gameObject.name);

        }
        */

        if ((hit_left && !hit_left.collider.isTrigger) ||
            (hit_right && !hit_right.collider.isTrigger) ||
            (hit_center && !hit_center.collider.isTrigger && hit_center.collider != gameObject.GetComponent<Collider2D>()))
        {

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }



        if (player_control)
        {
            Vector3 current_input = GetInput();
            float speed = (GetComponent<PlayerFloat>().is_floating ? float_movement_speed : ground_movement_speed);
            /*if(rb.velocity != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
            }*/
            transform.position += current_input * speed * Time.fixedDeltaTime;
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !GetComponent<PlayerFloat>().is_floating && isGrounded)
            {
                Jump();
            }
        }
        if(transform.position.y < -6.5f)
        {
            EventBus.Publish<GameOver>(new GameOver());
        }

    }

    Vector3 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        // can't control vertical movement when not floating
        if (!GetComponent<Floatable>().is_floating)
        {
            vertical_input = 0;
        }
        else
        {
            /*
            // can't move in diagonal direction
            if (horizontal_input != 0)
            {
                vertical_input = 0.0f;
            }
            if (vertical_input != 0)
            {
                horizontal_input = 0.0f;
            }
            */
        }

        playerDirection = new Vector2(horizontal_input, vertical_input);
        return new Vector3(horizontal_input, vertical_input, 0);
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        Debug.Log("jump");
    }
}