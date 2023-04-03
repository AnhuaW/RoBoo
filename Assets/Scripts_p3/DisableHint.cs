using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHint : MonoBehaviour
{
    public GameObject hint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GetComponent<Floatable>().is_floating)
        {
            GameObject.Destroy(hint);
            // disable the blinking script of brick
            if (GetComponent<Blinking>() != null)
            {
                GetComponent<Blinking>().enabled = false;
                StartCoroutine(SpriteTurnWhite(2f));
                //Debug.Log(GetComponent<SpriteRenderer>().color);
            }
            this.enabled = false;
        }
    }



    IEnumerator SpriteTurnWhite(float duration)
    {
        float initial_time = Time.time;

        while (Time.time - initial_time < duration)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return null;
        }
    }
}
