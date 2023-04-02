using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 5.0f;
    public float stopDistance = 2.0f;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        // Calculate the direction and distance to the player
        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // Move towards the player if they are far away
        if (distance > stopDistance)
        {
            direction.Normalize();
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }
}
