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
    private Planter current_planter;
    public Text money_text;
    public GameObject planter_prefab;
    private bool placing_planter = false;
    public InventoryItem held_item;

    public bool GetPlacement() 
    {
        return placing_planter;
    }

  
    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.SetPlayer(this);
        camera_offset = Camera.main.transform.position - transform.position;
        inventory.seed_list = ServiceLocator.GetGameManager().all_seeds;
        for (int i = 0; i < 8 && i < inventory.seed_list.Count; i++)
        {
            inventory.inventory_panel[i] = inventory.seed_list[i];
        }
        inventory.selected_crop = 0;
        held_seed = inventory.seed_list[0];
        ChangeSelection();
        UpdateUI();
    }

    private void UpdateUI() 
    {
        for (int i = 0; i < 8; i++) 
        {
            if (inventory.inventory_panel[i] == null)
            {
                item_slots[i].sprite = null;
            }
            else 
            {
                item_slots[i].sprite = inventory.inventory_panel[i].crop.sprite;
            }
        }
        money_text.text = inventory.money.ToString();
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
                    //if (placing_planter) 
                   // {
                    //    PlacePlanter(hit.point);
                   // }
                    //else 
                    //{ 
                    //GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    //marker.SetActive(true);
                    //marker.transform.position = hit.point;
                    //}
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && current_planter != null && held_seed != null) 
        {
            if (current_planter.HarvestCrop())
            {
                inventory.money += 10;
                UpdateUI();
            }
            else
            {
                current_planter.PlantCrop(held_seed.crop);
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
            //GetComponent<NavMeshAgent>().ResetPath();
            marker.SetActive(false);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * move_speed, Space.World);
            //GetComponent<NavMeshAgent>().ResetPath();
            marker.SetActive(false);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (camera_offset.y > 3)
            {
                camera_offset.y -= Input.GetAxis("Mouse ScrollWheel");
                camera_offset.z += Input.GetAxis("Mouse ScrollWheel") * 0.5f;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (camera_offset.y < 15)
            {
                camera_offset.y -= Input.GetAxis("Mouse ScrollWheel");
                camera_offset.z += Input.GetAxis("Mouse ScrollWheel") * 0.5f;
            }
        }
        Camera.main.transform.position = transform.position + camera_offset;


        bool select_item = false;
        int crop_id = 0;
        if (inventory.seed_list.Count == 0) 
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            select_item = true; 
            crop_id = inventory.selected_crop - 1;
            if (crop_id < 0) 
            {
                crop_id = 7;
            }
      
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            select_item = true;
            crop_id = inventory.selected_crop + 1;
            if (crop_id > 7)
            {
                crop_id = 0;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            crop_id = 0;
            if (inventory.seed_list.Count >= crop_id +1 && inventory.seed_list[crop_id] != null) 
            {
                select_item = true;
            }
        
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            crop_id = 1;
            if (inventory.seed_list.Count >= crop_id + 1 && inventory.seed_list[crop_id] != null)
            {
                select_item = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            crop_id = 2;
            if (inventory.seed_list.Count >= crop_id + 1 && inventory.seed_list[crop_id] != null)
            {
                select_item = true;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            crop_id = 3;
            if (inventory.seed_list.Count >= crop_id + 1 && inventory.seed_list[crop_id] != null)
            {
                select_item = true;
            }

        }

        if (select_item) 
        {
            inventory.selected_crop = crop_id;
            held_seed = inventory.inventory_panel[crop_id];
            ChangeSelection();
        }
    }

    private void FixedUpdate()
    {
        if (held_item != null)
        {
            held_item.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition - new Vector3(Screen.width, Screen.height, 0)/2;
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


    private void OnTriggerEnter(Collider other)
    {
        Planter p = other.gameObject.GetComponent<Planter>();
        if (p != null && current_planter != p) 
        {
            current_planter = p;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Planter p = other.gameObject.GetComponent<Planter>();
        if (p != null && current_planter == p) 
        {
            current_planter = null;
        }
    }


    private void PlacePlanter(Vector3 position) 
    {
        if (inventory.money >= 20)
        { 
            var overlaps = Physics.OverlapBox(position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, ~6);
            if (overlaps.Length > 0)
            {
                Vector3 offset = position - overlaps[0].transform.position;
                offset.y = 0;
                position += offset;
            }
            Instantiate(planter_prefab, position, Quaternion.identity);
            inventory.money -= 20;
            UpdateUI();
        }

    }

    public int SpendMoney(int money) 
    {
        inventory.money -= money;
        UpdateUI();
        return inventory.money;
    }


    public void TogglePlacement() 
    {
        if (inventory.money >= 20 || placing_planter)
        {
            placing_planter = !placing_planter;
        }
    }

    public void MoveItem(int previous_index, int new_index)
    {
        Seed item_1 = inventory.inventory_panel[previous_index];
        Seed item_2 = inventory.inventory_panel[new_index];
        inventory.inventory_panel[previous_index] = item_2;
        inventory.inventory_panel[new_index] = item_1;
    }

    public bool CheckItemInSlot(int slot_index) 
    {
        if (inventory.inventory_panel[slot_index] != null)
        {
            return true;
        }

        return false;
    
    }
}
