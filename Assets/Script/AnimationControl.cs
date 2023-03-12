using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    // Start is called before the first frame update
    Animator player_anim;
    ArrowKeyMovement player_movement;
    float inputX;
    float inputY;
    void Start()
    {
        player_anim = GetComponent<Animator>();
        player_movement = GetComponent<ArrowKeyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = player_movement.playerDirection.x;
        player_anim.SetFloat("inputX", inputX);
        inputY = player_movement.playerDirection.y;
        player_anim.SetFloat("inputY", inputY);
        if(inputX == 0 && inputY == 0)
        {
            player_anim.SetBool("idle", true);
        }
        else
        {
            player_anim.SetBool("idle", false);
        }
    }

}
