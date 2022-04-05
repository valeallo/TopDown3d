using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    private Crop planted_crop;
  
    public void PlantCrop(Crop crop)
    {
        if (planted_crop == null)
        {
            planted_crop = Instantiate(crop, transform.position, transform.rotation);
      
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrowCrop() 
    {
        if (planted_crop == null) 
        {
            return;
        }
        planted_crop.Grow();
    
    }

    public bool HarvestCrop() 
    {
        if (planted_crop != null)
        {
            bool harvestable = planted_crop.CheckHarvestability();
            if (harvestable)
            {
                Destroy(planted_crop.gameObject);
            }
            return harvestable;
        }

        return false;
    }


}
