using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLevel : MonoBehaviour
{
    GameObject load;
    [SerializeField] int build_index;
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
        StartCoroutine(load.GetComponent<levelLoder>().LoadLevel(build_index));
        
    }

}

