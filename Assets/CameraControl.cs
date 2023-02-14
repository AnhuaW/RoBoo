using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(moveUp());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(moveLeft());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(moveRight());
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(moveDown());
        }
    }

    IEnumerator moveUp()
    {
        Vector3 destination = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        transform.position = destination;
        yield return null;
    }

    IEnumerator moveDown()
    {
        Vector3 destination = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
        transform.position = destination;
        yield return null;
    }

    IEnumerator moveLeft()
    {
        Vector3 destination = new Vector3(transform.position.x - 11, transform.position.y, transform.position.z);
        transform.position = destination;
        yield return null;
    }

    IEnumerator moveRight()
    {
        Vector3 destination = new Vector3(transform.position.x + 11, transform.position.y, transform.position.z);
        transform.position = destination;
        yield return null;
    }
}
