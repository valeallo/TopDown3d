using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    protected NavMeshAgent navigation;
    protected float move_speed = 4;
    protected float hunger;
    protected float max_hunger;
    protected Vector3 destination; 
    // Start is called before the first frame update
    void Start()
    {
        //navigation = GetComponent<NavMeshAgent>();
        //navigation.speed = move_speed;
        destination = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Wander();
        
    }

    protected void Wander()
    {
        if (Vector3.Distance(transform.position, destination) < 2) 
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            destination = new Vector3(direction.x, 0, direction.y) * Random.Range(5, 20);
            //navigation.SetDestination(destination);
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, move_speed * Time.deltaTime);
        
    }
}
