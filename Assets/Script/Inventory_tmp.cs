using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Inventory_tmp : MonoBehaviour
{
    public static Inventory_tmp instance;
    public int bubble_ammo_count = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBubbleAmmo(int delta)
    {
        bubble_ammo_count += delta;
    }

    public int GetBubbleAmmo()
    {
        return bubble_ammo_count;
    }
}
