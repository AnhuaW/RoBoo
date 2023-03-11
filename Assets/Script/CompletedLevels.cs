using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class CompletedLevels : MonoBehaviour
{
    [SerializeField] int total_level_count = 3;
    public List<bool> level_completed;

    // Start is called before the first frame update
    void Start()
    {
        level_completed = Enumerable.Repeat(false, total_level_count).ToList();
        level_completed[0] = true;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
