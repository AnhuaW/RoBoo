using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Inventory inventory;
    public AudioClip door_open;
    bool played = false;
    void Start()
    {
        player = GameObject.Find("Player");
        inventory = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && inventory.get_chip_count() > 0)
        {
            inventory.use_chip(1);
            AudioSource.PlayClipAtPoint(door_open, Camera.main.transform.position);
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!played)
        {
            StartCoroutine(Unlock());
        }
    }

    IEnumerator Unlock()
    {
        //TODO: play audio
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        played = true;
    }
}
