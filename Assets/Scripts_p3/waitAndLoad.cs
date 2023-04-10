using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitAndLoad : MonoBehaviour
{
    public float time = 0.5f;
    public bool notLoaded = true;
    //wait before a period of time before loading
    private void Start()
    {
        enabled = false;
    }

    private void Update()
    {
        if (notLoaded)
        {
            waitThenLoad();
        }
    }
    IEnumerator waitThenLoad()
    {
        yield return new WaitForSeconds(time);
        enabled = true;
    }
}
