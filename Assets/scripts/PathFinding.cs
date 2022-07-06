using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private List<Tile> open = new List<Tile>();
    private List<Tile> closed = new List<Tile>();
    private List<Tile> path = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindPath(Tile start, Tile end) 
    {

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
        Debug.Log("no path found");
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
    }
}
