using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_detect_1 : MonoBehaviour
{
    // Start is called before the first frame update
    private int player_layerMask = 1 << 6;
    private Vector2 direction;
    RaycastHit2D isHit;
    RaycastHit2D isLeftHit;
    Rigidbody2D rb;
    public GameObject bullet_prefab;

    public float movement_speed = 8f;
    public float durationTime = 5f;
    public float lastTime = 0;
    public bool first = true;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime >= durationTime || first)
        {
            first = false;
            if (isHit)
            {
                Debug.Log("hit");
                StartCoroutine(Move(direction));
            }
            lastTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        direction = GetComponent<enemy1_move>().current_input;
        //Debug.Log(direction);
        isHit = Physics2D.Raycast(transform.position, transform.TransformDirection(direction), 3f, player_layerMask);
       // if (isHit) { Debug.Log("detect"); }
        
    }

    IEnumerator Move(Vector2 dir)
    {
        GameObject bullet;
        bullet = (GameObject)Instantiate(bullet_prefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
        Rigidbody2D bullet_v = bullet.gameObject.GetComponent<Rigidbody2D>();
        bullet_v.velocity = dir * movement_speed;

        yield return null;
    }
}
