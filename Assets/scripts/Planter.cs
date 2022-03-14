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
            Instantiate(crop, transform.position, transform.rotation);
            planted_crop = crop;
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
}
