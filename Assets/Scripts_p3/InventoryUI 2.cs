using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    Inventory_tmp curr_inventory;
    public Text ammoText;
    public Image ammoImage;
    void Start()
    {
        curr_inventory = GetComponent<Inventory_tmp>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curr_inventory.bubble_ammo_count > 0)
        {
            showInventory();
        }
        updateAmmoText();
    }

    void showInventory()
    {
        ammoText.enabled = true;
        ammoImage.GetComponent<Image>().enabled = true;
    }

    void updateAmmoText()
    {
        ammoText.text = "X " + curr_inventory.bubble_ammo_count;
    }
}
