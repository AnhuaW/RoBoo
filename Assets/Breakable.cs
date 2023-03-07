using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    GameObject player;
    float distance = 0f;
    int break_count = 0;
    public List<Sprite> breakable_tile_sprites;
    public AudioClip tile_break;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            distance = player.transform.position.y - transform.position.y;
        }

        change_tile_sprite(break_count);

        if(break_count == 2)
        {
            StartCoroutine(destroyTile());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (distance > 2.5f)
            {
                break_count++;
                AudioSource.PlayClipAtPoint(tile_break, Camera.main.transform.position);
            }
            distance = 0;
        }
    }

    void change_tile_sprite(int id)
    {
        GetComponent<SpriteRenderer>().sprite = breakable_tile_sprites[id];
    }

    IEnumerator destroyTile()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
