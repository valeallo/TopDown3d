using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Crop> all_crops = new List<Crop>();
    // Start is called before the first frame update
    void Awake()
    {
        ServiceLocator.SetGameManager(this); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
