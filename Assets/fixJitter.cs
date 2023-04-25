using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixJitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        
    }
}
