using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapAnimationControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target_pos;
    public float move_speed;
    public Animator playerAnim;
    
    void Start()
    {
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target_pos.position.x > transform.position.x)
        {
            playerAnim.SetFloat("inputX", 1);
        }

        else
        {
            playerAnim.SetFloat("inputX", -1);
        }
        transform.position = Vector3.Lerp(transform.position, target_pos.position, move_speed * Time.deltaTime);
    }
}
