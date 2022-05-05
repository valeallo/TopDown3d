using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public GameObject player;
    public GameObject tile_prefab;
    [SerializeField]private float tile_width;
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
        GenerateLevel();
    }

    private void GenerateLevel() 
    {
        Vector3 start_position = player.transform.position - new Vector3(tile_width, 0, tile_width) * radius;
        start_position /= tile_width;
        start_position = new Vector3(Mathf.RoundToInt(start_position.x), 0, Mathf.RoundToInt(start_position.z));
        start_position *= tile_width;
        for (int y = 0; y < radius * 2; y++) 
        {
            for (int x = 0; x < radius * 2; x++)
            {
                Vector3 position = start_position + new Vector3(x, 0, y) * tile_width;
                var overlaps = Physics.OverlapBox(position, new Vector3(0.5f, 0.025f, 0.5f), Quaternion.identity, ~7);
                if (overlaps.Length == 0)
                {
                    Instantiate(tile_prefab, position, Quaternion.identity);
                }
            }

        }
    }
}
