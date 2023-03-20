using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Light;
    //bool light = false;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") ){
            Light.SetActive(true);
        }
    }
    
}
