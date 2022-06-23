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
                //BuildPath() 
                open.Clear();
                closed.Clear();
                return;
            }
        }

    }
}
