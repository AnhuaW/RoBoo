using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changingColor : MonoBehaviour
{
    [SerializeField]
    private Color curr_color;
    public Color init_color;
    public Color changed_color;
    public KeyCode trigger;
    public bool mouseControl = false;
    // Start is called before the first frame update
    void Start()
    {
        init_color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        curr_color = GetComponent<SpriteRenderer>().color;

        if (!mouseControl)
        {

            if (Input.GetKeyDown(trigger))
            {
                GetComponent<SpriteRenderer>().color = changed_color;
            }

            if (Input.GetKeyUp(trigger))
            {
                GetComponent<SpriteRenderer>().color = init_color;
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<SpriteRenderer>().color = changed_color;
            }
            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<SpriteRenderer>().color = init_color;
            }
        }
        
    }

}
