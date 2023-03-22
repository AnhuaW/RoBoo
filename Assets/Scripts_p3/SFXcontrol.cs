using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXcontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip collectAmmo;
    public AudioClip portal;
    public AudioClip recharge;
    public AudioClip key;
    public float clipVolume = 0.3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("battery"))
        {
            AudioSource.PlayClipAtPoint(collectAmmo, Camera.main.transform.position,clipVolume);
        }

        if (other.CompareTag("Portal"))
        {
            AudioSource.PlayClipAtPoint(portal, Camera.main.transform.position,clipVolume);
        }

        if (other.CompareTag("firstAmmo"))
        {
            AudioSource.PlayClipAtPoint(recharge, Camera.main.transform.position,clipVolume);
        }

        if (other.CompareTag("key"))
        {
            AudioSource.PlayClipAtPoint(key, Camera.main.transform.position);
        }
    }
}
