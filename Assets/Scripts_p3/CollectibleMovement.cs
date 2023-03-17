using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float amplitude = 0.5f;
    public float direction = -1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * amplitude * direction, 0);
    }
}
