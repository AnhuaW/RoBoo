using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redbullet_control : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall")) {
            Destroy(this.gameObject);
        }

    }
}
