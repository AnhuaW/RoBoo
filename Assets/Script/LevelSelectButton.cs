using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    GameObject load;
    public int level_index;
    // 1-based level_index should be equal to 0-based build index
    

    CompletedLevels completed_levels_list;

    // Start is called before the first frame update
    void Start()
    {
        completed_levels_list = CompletedLevels.instance;
        load = GameObject.Find("levelLoader");
    }

    // Update is called once per frame
    void Update()
    {
        if(completed_levels_list && completed_levels_list.level_completed[level_index - 1] != true)
        {
            gameObject.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    public void LoadSceneOnClick()
    {
        if (completed_levels_list != null && completed_levels_list.level_completed[level_index - 1] == true)
        {
            StartCoroutine(load.GetComponent<levelLoder>().LoadLevel(level_index));
            //SceneManager.LoadScene(level_index);
        }
    }

    
}
