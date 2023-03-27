using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    //List of sprites
    public List<Sprite> sprites;
    //changing interval
    public float interval = 0.2f;
    bool changing;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!changing)
        {
            StartCoroutine(change());
        }
    }

    IEnumerator change()
    {
        changing = true;
        foreach (Sprite sprite in sprites)
        {
            Debug.Log(sprite.name);
            GetComponent<SpriteRenderer>().sprite =  sprite;
            yield return new WaitForSeconds(interval);
        }
        changing = false;
        //yield return null;
    }
}
