using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to bind laser emitter to brick
public class BoundToBrick : MonoBehaviour
{
    public Transform brick;
    [SerializeField] Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - brick.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = brick.position + offset;
    }
}
