using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_control : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bullet");
        //GameObject collider = collision.collider.gameObject;
        Destroy(this.gameObject);
    }
}
