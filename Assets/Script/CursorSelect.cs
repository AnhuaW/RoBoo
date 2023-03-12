using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// change sprite color when mouse cursor is over the object
public class CursorSelect : MonoBehaviour
{
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        rend.material.color = Color.cyan;
    }

    private void OnMouseExit()
    {
        rend.material.color = Color.white;
    }
}
