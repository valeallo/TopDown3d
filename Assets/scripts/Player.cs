using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject marker;
    private Vector3 camera_offset;
    private float move_speed = 10f;

    public Seed held_seed;
    private Inventory inventory = new Inventory();
    public Image[] item_slots = new Image[8];
    public Image[] item_slots_background = new Image[8];

  
    // Start is called before the first frame update
    void Start()
    {
        camera_offset = Camera.main.transform.position - transform.position;
        inventory.seed_list = ServiceLocator.GetGameManager().all_seeds;
        for (int i = 0; i < 8 && i < inventory.seed_list.Count; i++)
        {
            inventory.inventory_panel[i] = inventory.seed_list[i];
        }
        UpdateUI();
    }

    private void UpdateUI() 
    {
        for (int i = 0; i < 8; i++) 
        {
            if (inventory.inventory_panel[i] != null)
            {
                item_slots[i].sprite = inventory.inventory_panel[i].crop.sprite;
            }
        }
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

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            int crop_id = inventory.selected_crop - 1;
            if (crop_id < 0) 
            {
                crop_id = inventory.seed_list.Count - 1;
            }
            inventory.selected_crop = crop_id;
            held_seed = inventory.seed_list[crop_id];
            ChangeSelection();
        }
    }

    private void ChangeSelection() 
    {
        foreach (var item in item_slots_background) 
        {
            item.GetComponent<Image>().color = Color.white;
        }
        item_slots_background[inventory.selected_crop].GetComponent<Image>().color = Color.yellow;    
    }
    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collision.gameObject.GetComponent<Planter>() != null ) 
            {
                collision.gameObject.GetComponent<Planter>().PlantCrop(held_seed.crop);
            }
            
        }
    }
}
