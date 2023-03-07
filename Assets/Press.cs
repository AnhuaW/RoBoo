using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    GravityControl gravity_status;
    public GameObject portal;
    public AudioClip collision;
    void Start()
    {
        player = GameObject.Find("Player");
        gravity_status = player.GetComponent<GravityControl>();
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("block") && gravity_status.enable_gravity)
        {
            portal.SetActive(true);
            other.gameObject.GetComponent<GravityControl>().enabled = false;
            AudioSource.PlayClipAtPoint(collision, Camera.main.transform.position);
        }
    }
}
