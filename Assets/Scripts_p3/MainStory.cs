using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStory : MonoBehaviour
{
    public GameObject load;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        load = GameObject.Find("levelLoader");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneOnClick()
    {
        Debug.Log("click");
        StartCoroutine(load.GetComponent<levelLoder>().LoadLevel(index));
    }
}
