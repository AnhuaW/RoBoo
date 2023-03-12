using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LIghtController : MonoBehaviour
{
    [SerializeField] GameObject GlobalLight;
    // Start is called before the first frame update
    void Start()
    {
       if(GlobalLight == null)
        {
            Debug.Log("Missing Global Light");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && GlobalLight != null)
        {
            GlobalLight.GetComponent<Light2D>().enabled = !GlobalLight.GetComponent<Light2D>().enabled;
        }
    }
}
