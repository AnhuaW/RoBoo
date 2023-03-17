using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

}
