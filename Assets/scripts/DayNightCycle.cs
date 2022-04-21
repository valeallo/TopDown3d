using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    private float day_length = 3f;
    private float current_time = 0;
    public Text time_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(180/day_length * Time.fixedDeltaTime, 0, 0));
        current_time += Time.fixedDeltaTime; // 12 * 0.02
        if (current_time >= day_length * 2) 
        { 
            current_time = current_time - day_length * 2;
            foreach (var p in FindObjectsOfType<Planter>()) 
            {
                p.GrowCrop();
            }
        }
        time_text.text = Mathf.Round(current_time).ToString();
    }
}
