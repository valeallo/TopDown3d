using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PathFinding))]
public class Animal : MonoBehaviour
{
    protected PathFinding pathFinding;
    protected float move_speed = 4;
    protected float hunger;
    protected float max_hunger;
    protected Vector3 destination;
    List<Tile> previous_path = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        //navigation = GetComponent<NavMeshAgent>();
        //navigation.speed = move_speed;
        destination = transform.position;
        pathFinding = GetComponent<PathFinding>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ServiceLocator.GetLevelGen().GetTiles().Count == 0)
        {
            return;
        }
       
        if (transform.position != destination && pathFinding.GetPath().Count > 0 && pathFinding.path_ready)
        {
           if (transform.position == pathFinding.GetPath()[0].transform.position) 
            {
                pathFinding.GetPath().RemoveAt(0); 
            }
           else
            {
                transform.position = Vector3.MoveTowards(transform.position, pathFinding.GetPath()[0].transform.position, move_speed * Time.deltaTime);
            }
        }
        else
        {
            Wander();
        }
        
    }

    protected void Wander()
    {
        
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 d =  transform.position + new Vector3(direction.x, 0, direction.y) * Random.Range(5, 10);
        MoveTo(d);
        
        
    }

    protected void MoveTo(Vector3 position)
    {
        pathFinding.FindPath(transform.position, position);
        destination = position;
        foreach (var t in previous_path)
        {
            t.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        foreach(var t in pathFinding.GetPath())
        {
            t.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        previous_path = pathFinding.GetPath();
    }
}
