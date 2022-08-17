using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private List<Tile> open = new List<Tile>();
    private List<Tile> closed = new List<Tile>();
    private List<Tile> path = new List<Tile>();
    public bool path_ready = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FindPath(Vector3 start, Vector3 end)
    {
        Tile start_tile, end_tile;
        List<Tile> tiles = ServiceLocator.GetLevelGen().GetTiles();
        start_tile = tiles[0];
        end_tile = tiles[0];
        float start_distance = Vector3.Distance(start, start_tile.transform.position);
        float end_distance = Vector3.Distance(end, end_tile.transform.position);
        for ( int i = 1; i < tiles.Count; i++)
        {
            float s_dist = Vector3.Distance(tiles[i].transform.position, start);
            if (s_dist < start_distance)
            {
                start_distance = s_dist;
                start_tile = tiles[i];
            }
            float e_dist = Vector3.Distance(tiles[i].transform.position, end);
            if (e_dist < end_distance)
            {
                end_distance = e_dist;
                end_tile = tiles[i];
            }

        }
        FindPath(start_tile, end_tile);
    }
    public void FindPath(Tile start, Tile end) 
    {
        path_ready = false;
        path.Clear();
        ServiceLocator.GetLevelGen().ResetCost();
        open.Add(start);
        start.g_cost = 0;
        while (open.Count > 0)
        {
            Tile current = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].f_cost < current.f_cost) 
                {
                    current = open[i];
                }
            }
            open.Remove(current);
            closed.Add(current);
            if (current == end)
            {
                BuildPath(start, end);
                open.Clear();
                closed.Clear();
                return;
            }
            foreach (var n in current.neighbours)
            {
                if (closed.Contains(n) || !n.walkable)
                {
                    continue;
                }
                float move_cost = current.g_cost + Vector2.Distance(current.grid_position, n.grid_position);
                if (move_cost <  n.g_cost) 
                {
                    n.g_cost = move_cost;
                    n.h_cost = Vector2.Distance(n.grid_position, end.grid_position);
                    n.SetParent(current);
                    if (!open.Contains(n)) 
                    {
                        open.Add(n);
                    }
                }
            }
        }
        open.Clear();
        closed.Clear();
       
    }
    private void BuildPath(Tile start, Tile end) 
    {
        Tile current = end;
        while (current != start) 
        {
            path.Add(current);
            current = current.GetParent();
        }
        
        path.Reverse();
        path_ready = true;
    }

    public List<Tile> GetPath() 
    {
        return path; 
    }
}
