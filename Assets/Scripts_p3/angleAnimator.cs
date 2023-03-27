using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleAnimator : MonoBehaviour
{
    Animator animator;
    statue_attack angle;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        angle = GetComponent<statue_attack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (angle.isGrounded)
        {

        }
    }
}
