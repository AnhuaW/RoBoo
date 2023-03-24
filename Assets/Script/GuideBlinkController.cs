using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBlinkController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrompeDisplay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrompeDisplay(){ //TODO: change to typing?
        
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }
}
