using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;

public class Inventory_tmp : MonoBehaviour
{
    public static Inventory_tmp instance;
    public int bubble_ammo_count = 0; // TODO: make it private later
    public int initial_bubble_ammo_count = 0; // bubble ammo count at the beginning of this level
    public int key_count = 0;
    public int initial_key_count = 0;
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
        //DontDestroyOnLoad(gameObject);

        //RecordInitialState();

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

    public void ChangeKey(int delta)
    {
        key_count += delta;
    }

    public int GetKey()
    {
        return key_count;
    }

    public void RecordInitialState()
    {
        initial_bubble_ammo_count = bubble_ammo_count;
        initial_key_count = key_count;
    }

    public void RetrieveInitialState()
    {
        bubble_ammo_count = initial_bubble_ammo_count;
        key_count = initial_key_count;
    }

    public void ClearAll()
    {
        bubble_ammo_count = 0;
        key_count = 0;
    }



    private void OnDestroy()
    {
        
    }

}

