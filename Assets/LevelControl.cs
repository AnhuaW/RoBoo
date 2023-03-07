using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int level_idx = 0;
    public List<GameObject> level;
    Vector3 curr_map_pos;
    GameObject curr_map;
    public Text curr_level;
    public AudioClip transport;
    void Start()
    {
        level_idx = 0;
        curr_map = GameObject.FindGameObjectWithTag("Level");
        curr_map_pos = curr_map.transform.position;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal"))
        {
            StartCoroutine(LevelProgression());
        }
        
    }

    IEnumerator LevelProgression()
    {
        level_idx++;
        Debug.Log("Teleporting to level " + level_idx);
        AudioSource.PlayClipAtPoint(transport, Camera.main.transform.position);
        //yield return new WaitForSeconds(0.3f);
        curr_map = GameObject.FindGameObjectWithTag("Level");
        GameObject.Destroy(curr_map);
        GenerateMap();
        yield return null;
    }

    void GenerateMap()
    {
        Debug.Log("curr_idx: " + level_idx);
        GameObject.Instantiate(level[level_idx],curr_map_pos,Quaternion.identity);
        curr_level.text = "Level - " + level_idx;
    }
}
