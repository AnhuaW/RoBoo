using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAngle : MonoBehaviour
{
    Camera cam;
    public GameObject prefab;
    bool first = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!first && cam.transform.position.x > 14.38f && cam.transform.position.y == 7f)
        {
            GameObject.Instantiate(prefab, new Vector3(25.5f, 9.247638f, 0f), Quaternion.identity);
            first = true;
        }
    }
}
