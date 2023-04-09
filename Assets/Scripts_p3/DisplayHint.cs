using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hint;
    public bool collided;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        hint.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collided = true;
        Debug.Log("hint collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("displaying hint");
            hint.active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collided = false;
        if (other.gameObject.CompareTag("Player"))
        {
            hint.active = false;
        }
    }
}
