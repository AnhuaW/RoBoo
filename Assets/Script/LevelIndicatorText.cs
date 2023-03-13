using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelIndicatorText : MonoBehaviour
{
    int text_index = 0;
    Text indicator_text;

    // Start is called before the first frame update
    void Start()
    {
        indicator_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int current_index = SceneManager.GetActiveScene().buildIndex;
        if(text_index != current_index)
        {
            text_index = current_index;
            indicator_text.text = "Level - " + text_index.ToString();
        }
    }
}
