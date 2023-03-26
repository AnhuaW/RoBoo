using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    //public float posX = 0f;
    public float offsetY = 3f;
    public float cameraSpeed = 5.0f;
    //Camera x, y boundaries
    public float minX = 0.0f;
    public float maxX = 19f;
    public float minY = -1.0f;
    public float maxY = 9.0f;
    [SerializeField]
    Vector3 camera_target_pos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float clampedX = Mathf.Clamp(player.transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(player.transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the camera to track player currX and currY
        camera_target_pos = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, camera_target_pos  , cameraSpeed * Time.deltaTime);

        // Clamp the camera's position within the scene boundaries
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        //check if player fell out of the scene
        if(player.transform.position.y  <  transform.position.y - Camera.main.orthographicSize - 1)
        {
            EventBus.Publish<GameOver>(new GameOver());
        }
    }
}


   