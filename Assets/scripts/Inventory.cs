using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Crop> crop_list;
    public int selected_crop;
    public Crop[] inventory_panel;

    public Inventory() 
    {
        crop_list = new List<Crop>();
        inventory_panel = new Crop[8];
        selected_crop = 0;
    }
}
