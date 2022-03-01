using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject marker;
    private Vector3 camera_offset;
    // Start is called before the first frame update
    void Start()
    {
        camera_offset = Camera.main.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit)) 
            {
                if (hit.transform.name == "Plane") 
                {
                    GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    marker.transform.position = hit.point;
                }
            
            }
        
        
        }

        Vector3 look_direction = transform.forward;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            look_direction.z = 1;

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            look_direction.z = -1;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            look_direction.x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            look_direction.x = -1;
        }

        look_direction.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(look_direction, Vector3.up);
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 ) 
        {
            if (camera_offset.y > 3) 
            {
                camera_offset.y -= Input.GetAxis("Mouse ScrollWheel");
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (camera_offset.y < 15)
            {
                camera_offset.y -= Input.GetAxis("Mouse ScrollWheel");
            }
        }
        Camera.main.transform.position = transform.position + camera_offset;
    }
}
