using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip gravity_on;
    public AudioClip gravity_off;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Gravity Control Audio */
        if(Input.GetKeyDown(KeyCode.E))
            
        {
            if (GetComponent<GravityControl>().gravity_enabled && 
                GetComponent<Inventory>().get_energy_level() > 0)
            {
                AudioSource.PlayClipAtPoint(gravity_on, Camera.main.transform.position);
            }

            else if(!GetComponent<GravityControl>().gravity_enabled &&
                     GetComponent<Inventory>().get_energy_level() > 0) 
            {
                AudioSource.PlayClipAtPoint(gravity_off, Camera.main.transform.position);
            }
        }
    }
}
