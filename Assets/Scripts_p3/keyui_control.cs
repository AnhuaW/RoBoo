using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyui_control : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject keys;
    Color ori_color = Color.white;
    void Start()
    {
        keys = GameObject.Find("keys_ui");
        if(keys != null)
        {
            Debug.Log("1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate_key(int index)
    {
        GameObject key_ui = keys.transform.GetChild(index).gameObject;
        key_ui.GetComponent<Image>().color = ori_color;
    }
}
