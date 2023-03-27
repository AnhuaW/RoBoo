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

    private bool stop = false;

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
        if (movingToTarget && !stop)
        {
            // move to target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // back to original pos
            if (transform.position == targetPosition)
            {
                movingToTarget = false;
                Debug.Log("notToTarget");
                StartCoroutine(MoveToStart(waitTime));
            }
        }
        else if(!movingToTarget && !stop)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

            if (transform.position == startPosition)
            {
                Debug.Log("ToTarget");
                movingToTarget = true;
                StartCoroutine(MoveToTarget(waitTime));
            }
        }
    }

    IEnumerator MoveToStart(float waitTime)
    {
        Debug.Log("wait");
        stop = true;
        yield return new WaitForSeconds(waitTime);
        targetPosition = startPosition;
        stop = false;
    }

    IEnumerator MoveToTarget(float waitTime)
    {
        Debug.Log("wait");
        stop = true;
        yield return new WaitForSeconds(waitTime);
        targetPosition = startPosition + new Vector3(horizontal_range, vertical_range, 0f);
        stop = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
