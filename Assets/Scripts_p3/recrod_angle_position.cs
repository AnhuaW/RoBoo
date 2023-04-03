using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recrod_angle_position : MonoBehaviour
{
    public GameObject angle;
    private Vector3 ori_pos = Vector3.zero;
    private bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(first && angle.active)
        {
            ori_pos = angle.transform.position;
            first = false;
        }
    }

    public Vector3 getPosition()
    {
        return ori_pos;
    }
}
