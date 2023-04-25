using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            /*
            GameObject completed_levels = GameObject.Find("CompletedLevels");
            if (completed_levels != null)
            {
                CompletedLevels c = completed_levels.GetComponent<CompletedLevels>();
                for (int i = 0; i < c.level_completed.Count; ++i)
                {
                    c.level_completed[i] = true;
                }
            }
            */
            if (CompletedLevels.instance != null)
            {
                for (int i = 0; i < CompletedLevels.instance.level_completed.Count; ++i)
                {
                    CompletedLevels.instance.level_completed[i] = true;
                }
            }
        }
    }
}
