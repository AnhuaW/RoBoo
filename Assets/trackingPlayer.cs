using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float posX = 0f;
    public float posY = 3f;
    public float cameraSpeed = 5.0f;
    //Camera x, y boundaries
    public float minX = -1.0f;
    public float maxX = 20f;
    public float minY = -2.0f;
    public float maxY = 10.0f;

    void Start()
    {
        //player = GameObject.Find("Player");
        float clampedX = Mathf.Clamp(player.transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(player.transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the camera to track player currX and currY
        Vector3 camera_target_pos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, camera_target_pos  , cameraSpeed * Time.deltaTime);

        // Clamp the camera's position within the scene boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
   
}


   