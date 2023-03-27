using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax: MonoBehaviour
{
    public GameObject background;  // Array of background layers to be parallaxed
    public float parallaxScale = 0.5f;  // How much to parallax the backgrounds
    public float parallaxReductionFactor = 0.5f;  // How quickly the parallax effect reduces
    public float smoothing = 1f;  // How smooth the parallax effect should be

    private Vector3 lastPosition;  // The position of the camera in the last frame

    private void Start()
    {
        lastPosition = transform.position;  // initialize camera lastPostion
    }

    private void Update()
    {
        float parallax = (lastPosition.x - transform.position.x) * parallaxScale;  // Calculate the parallax amount

            // Calculate the target position for the background layer
            Vector3 targetPosition = background.transform.position + Vector3.right * parallax;

            // Lerp the background layer towards the target position for a smooth parallax effect
            background.transform.position = Vector3.Lerp(background.transform.position, targetPosition, smoothing * Time.deltaTime);

        lastPosition = transform.position;  // Set the last position to the camera's current position
    }
}