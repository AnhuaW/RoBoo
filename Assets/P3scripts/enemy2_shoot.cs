using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2_shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject player;
    public float movement_speed = 4f;
    public float durationTime = 10f;
    public float lastTime = 0;
    private bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime >= durationTime || first)
        {
            first = false;
            StartCoroutine(Move());
            lastTime = Time.time;
        }
    }

    IEnumerator Move()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject bullet, bullet_2, bullet_3;
        bullet = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.Euler(0f, 0f, angle));
        Rigidbody2D bullet_v = bullet.gameObject.GetComponent<Rigidbody2D>();
        bullet_v.velocity = dir * movement_speed;
        yield return null;
    }
}
