using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 3f; // The speed at which the platform moves
    public float distance = 5f; // The distance the platform moves horizontally and vertically
    public float waitTime = 2f; // The amount of time the platform waits at the end of each movement

    private Vector3 startPosition; 
    private Vector3 targetPosition; 
    private bool movingToTarget = true; // if the platform is moving to target

    public bool horizotal_move = true;
    public bool vertical_move = false;

    private float horizontal_range = 0;
    private float vertical_range = 0;

    private void Start()
    {
        startPosition = transform.position;
        //check the desired moving direction
        if (horizotal_move)
        {
            horizontal_range = distance;
        }

        if (vertical_move)
        {
            vertical_range = distance;
        }
        //if both horizontal & vertical the platform can move in diagonal
        targetPosition = startPosition + new Vector3(horizontal_range, vertical_range, 0f);
    }

    private void Update()
    {
        if (movingToTarget)
        {
            // move to target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // back to original pos
            if (transform.position == targetPosition)
            {
                movingToTarget = false;
                Invoke("MoveToStart", waitTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

            if (transform.position == startPosition)
            {
                movingToTarget = true;
                Invoke("MoveToTarget", waitTime);
            }
        }
    }

    private void MoveToStart()
    {
        targetPosition = startPosition;
    }

    private void MoveToTarget()
    {
        targetPosition = startPosition + new Vector3(horizontal_range, vertical_range, 0f);
    }
}
