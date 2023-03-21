using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum collectible_types { bubble_ammo, key};
public class Collectible : MonoBehaviour
{

    public collectible_types type;
    Inventory_tmp inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory_tmp.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            switch (type)
            {
                case collectible_types.bubble_ammo:
                    inventory.ChangeBubbleAmmo(1);
                    Destroy(gameObject);
                    break;

                case collectible_types.key:
                    inventory.ChangeKey(1);
                    Destroy(gameObject);
                    break;
            }

            //Destroy(gameObject);
        }
    }

}
