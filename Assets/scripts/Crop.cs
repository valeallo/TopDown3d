using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public List<GameObject> growthStages = new List<GameObject>();
    private int growth_stage_number = 0;
    public Sprite sprite;
    public void Grow()
    {
        growth_stage_number++;
        if (growth_stage_number >= growthStages.Count) 
        {
            growth_stage_number = growthStages.Count - 1;
        
        }
        growthStages[growth_stage_number].SetActive(true);
        growthStages[growth_stage_number -1 ].SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
