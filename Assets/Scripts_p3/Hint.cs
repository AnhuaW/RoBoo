using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    // Start is called before the first frame update
    bool blinking = false;
    public float interval = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!blinking)
        {
            StartCoroutine(hintBlink());
        }
    }

    IEnumerator hintBlink()
    {
        blinking = true;
        GetComponent<Text>().enabled = false;
        yield return new WaitForSeconds(interval);
        GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(interval);
        blinking = false;
    }
}
