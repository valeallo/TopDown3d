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
    private Vector3 previous_spawn_position = new Vector3();
    public GameObject rock_prefab;
    private void Awake()
    {
        ServiceLocator.SetLevelGen(this);
        GenerateLevel();
        previous_spawn_position = player.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Tile> GetTiles()
    {
        return tiles;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(player.transform.position, previous_spawn_position)> tile_width * 5)
        {
            GenerateLevel();
            previous_spawn_position = player.transform.position;
        }

    }

    private void GenerateLevel() 
    {
        Vector3 start_position = player.transform.position - new Vector3(tile_width, 0, tile_width) * radius;
        start_position /= tile_width;
        start_position = new Vector3(Mathf.RoundToInt(start_position.x), 0, Mathf.RoundToInt(start_position.z));
        start_position *= tile_width;
        List<Tile> new_tiles = new List<Tile>();
        for (int y = 0; y < radius * 2; y++) 
        {
            for (int x = 0; x < radius * 2; x++)
            {
                Vector3 position = start_position + new Vector3(x, 0, y) * tile_width;
                var overlaps = Physics.OverlapBox(position, new Vector3(0.5f, 0.025f, 0.5f), Quaternion.identity, ~7);
                if (overlaps.Length == 0)
                {
                    GameObject chosen_prefab = tile_prefab;
                    if (Random.Range(0, 100) < 5) 
                    {
                        chosen_prefab = rock_prefab;
                    }
                    Tile new_tile = Instantiate(chosen_prefab, position, Quaternion.identity).GetComponent<Tile>();
                    tiles.Add(new_tile);
                    new_tiles.Add(new_tile);
                    position /= 2f;
                    new_tile.grid_position = new Vector2Int((int)position.x, (int)position.z);
                    new_tile.name = new_tile.grid_position.ToString();
                   
                }
            }

        }
        foreach(var t in new_tiles)
        {
           
            for (int y = -1; y < 2; y++)
            {
                for(int x = -1; x < 2; x++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    Tile neighbour;
                    if (FindTile(t.grid_position + new Vector2Int(x, y), out neighbour))
                    {
                        t.neighbours.Add(neighbour);
                        neighbour.neighbours.Add(t);
                        Debug.Log("neighbour added");
                    }
                }
            }
        }
    }

    public bool FindTile(Vector2Int grid_position, out Tile tile)
    {
        tile = null;
        foreach (var t in tiles)
        {
            if (t.grid_position == grid_position)
            {
                tile = t;
                Debug.Log("tile found");
                return true;
            }
        }
        return false;
    }

    public void ResetCost()
    {
        foreach (var t in tiles) 
        {
            t.g_cost = Mathf.Infinity;
        }
    }
}
