using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject player;
    public GameObject tile_prefab;
    private float tile_width = 1f;
    private List<Tile> tiles = new List<Tile>();
    private int radius = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void GenerateLevel() 
    {
        Vector3 start = player.transform.position - new Vector3(tile_width, 0, tile_width) * radius;
        for (int y = -radius; y < radius; y++) 
        {
            for (int x = -radius; x < radius; x++)
            {

            }

        }
    }
}
