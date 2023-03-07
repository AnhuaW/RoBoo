using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConsumption : MonoBehaviour
{
    // Start is called before the first frame update
    Inventory inventory;
  
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Gravity Control Audio */
        if (Input.GetKeyDown(KeyCode.E))

        {
            Debug.Log("E pressed");
            if (GetComponent<GravityControl>().enable_gravity &&
                GetComponent<Inventory>().get_energy_level() > 0)
            {
                Debug.Log("consuming energy");
                inventory.decrease_energy(1);
            }

        }
    }
}
