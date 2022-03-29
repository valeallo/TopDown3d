using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Seed> seed_list;
    public int selected_crop;
    public Seed[] inventory_panel;

    public Inventory() 
    {
        seed_list = new List<Seed>();
        inventory_panel = new Seed[8];
        selected_crop = 0;
    }
}
