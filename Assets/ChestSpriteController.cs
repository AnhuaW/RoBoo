using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpriteController : MonoBehaviour
{
    // Start is called before the first frame update
    public float wait = 1f;
    public Sprite emptyChest;
    public GameObject door;
    bool opened = false;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
        GetComponent<SpriteChange>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!opened)
        {
            StartCoroutine(openChest());
        }
    }

    IEnumerator openChest()
    {
        player.GetComponent<ArrowKeyMovement>().player_control = false;
        player.GetComponent<Animator>().enabled = false;
        GetComponent<SpriteChange>().enabled = true;
        yield return new WaitForSeconds(wait);
        GetComponent<SpriteChange>().enabled = false;
        Destroy(GetComponent<ParticleSystem>());
        //TODO door
        opened = false;
        door.transform.position = player.transform.position;
    }
}
