using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    private int energy_level = 0;
    private int chip_count = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void increase_energy(int level)
    {
        energy_level += level;
    }

    public void decrease_energy(int level)
    {
        energy_level -= level;
    }

    public int get_energy_level()
    {
        return energy_level;
    }

    public void add_chip(int chip_num)
    {
        chip_count += chip_num;
    }

    public void use_chip(int chip_num)
    {
        chip_count-= chip_num;
    }

    public int get_chip_count()
    {
        return chip_count;
    }
}
