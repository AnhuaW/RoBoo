using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAngle : MonoBehaviour
{
    Camera cam;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("statue") && cam.transform.position.x > 11f && cam.transform.position.y > 10f)
        {
            prefab.SetActive(true);
            cam.GetComponent<trackingPlayer>().offsetY = 2;
        }
    }
}
