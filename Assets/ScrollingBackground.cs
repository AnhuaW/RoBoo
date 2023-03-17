using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftEdge;
    public GameObject rightEdge;
    GameObject player;
    [SerializeField]
    float moveX;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        moveX = player.GetComponent<ArrowKeyMovement>().playerDirection.x;
        Debug.Log(player.GetComponent<ArrowKeyMovement>().playerDirection.x);
    }

    void StartScroll()
    {
        return;
    }

    bool ReachEnd()
    {
        return false;
    }
}
