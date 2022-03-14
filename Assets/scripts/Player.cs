using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject marker;
    private Vector3 camera_offset;
    private float move_speed = 10f;
    public GameObject projectile;
    public Crop held_crop;
  
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

        //look_direction.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(look_direction, Vector3.up);

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward* Input.GetAxis("Vertical") * Time.deltaTime * move_speed, Space.World);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * move_speed, Space.World);
        }



        if (Input.GetKey(KeyCode.Space))
        {
            GameObject p = Instantiate(projectile, transform.position, transform.rotation);
            p.GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);
        
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
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

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.GetComponent<Planter>() != null ) 
            {
                Debug.Log("planted");
                collision.gameObject.GetComponent<Planter>().PlantCrop(held_crop);
            }
            
        }
    }
}
