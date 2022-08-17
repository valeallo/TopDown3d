using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PathFinding))]
public class Animal : MonoBehaviour
{
    protected PathFinding pathFinding;
    protected float move_speed = 4;
    [SerializeField] protected float hunger = 0;
    protected float max_hunger = 100;
    protected Vector3 destination;
    [SerializeField] private bool moving_to_food = false;
    private Planter chosen_planter;
    List<Tile> previous_path = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        //navigation = GetComponent<NavMeshAgent>();
        //navigation.speed = move_speed;
        destination = transform.position;
        pathFinding = GetComponent<PathFinding>();
        StartCoroutine(HungerCreator());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ServiceLocator.GetLevelGen().GetTiles().Count == 0)
        {
            return;
        }
        if (moving_to_food && Vector3.Distance(destination, transform.position) < 0.5f)
        {
            Debug.Log("food is near");
            EatFood();
        }
        if (transform.position != destination && pathFinding.GetPath().Count > 0 && pathFinding.path_ready)
        {
            Debug.Log("moving on the path");
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
            if (hunger < max_hunger / 4)
            {
                Wander();
            }
            else if (!moving_to_food)
            {
                if (SearchFood()) 
                {
                    Wander();
                }
                Debug.Log("SearchFood");

            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, move_speed * Time.deltaTime);
            }

        }
        
    }

    protected void Wander()
    {
        
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 d =  transform.position + new Vector3(direction.x, 0, direction.y) * Random.Range(5, 20);
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
        previous_path.Clear();
        previous_path.AddRange(pathFinding.GetPath());
    }


    private IEnumerator HungerCreator()
    {
        hunger++;
        if (hunger > max_hunger)
        {
            hunger = max_hunger;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(HungerCreator());
    }

    protected bool SearchFood(float radius = 10) 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        List<Planter> planters_in_range = new List<Planter>();
        foreach (var c in colliders)
        {
            if( c.GetComponent<Planter>() != null && c.GetComponent<Planter>().IsCropPlanted())
            {
                planters_in_range.Add(c.GetComponent<Planter>());
            }
        }
        if (planters_in_range.Count > 0)
        {
            Planter closest = planters_in_range[0];
            float closest_distance = Vector3.Distance(closest.transform.position, transform.position);
            for (int i = 1; i < planters_in_range.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, planters_in_range[i].transform.position);
                if (distance < closest_distance ) 
                {
                    closest = planters_in_range[i];
                    closest_distance = distance;
                }
            }
            chosen_planter = closest;
            pathFinding.FindPath(transform.position, closest.transform.position);
            moving_to_food = true;
            return true;
        }
        else
        {
            Debug.Log("food not found");
            return false;
            
        }
    }


    private void EatFood() 
    {
        Debug.Log("eatFood");
        if (chosen_planter.HarvestCrop())
        {
            hunger -= 100;
            if (hunger < 0)
            {
                hunger = 0;
            }
            Debug.Log("eat food succesful");
        }
        moving_to_food = false;
    }

}
