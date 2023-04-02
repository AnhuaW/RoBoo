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
            this.enabled = false;
        }
    }
}
