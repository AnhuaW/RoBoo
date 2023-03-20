using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    Inventory_tmp curr_inventory;
    public Slider ammoBar;
    float curr_ammo;
    void Start()
    {
        curr_inventory = GetComponent<Inventory_tmp>();
        curr_ammo = curr_inventory.GetBubbleAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        curr_ammo = curr_inventory.GetBubbleAmmo();
        ammoBar.value = curr_ammo;
    }
}
