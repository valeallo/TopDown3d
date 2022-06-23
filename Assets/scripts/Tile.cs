using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Vector2Int grid_position;
    public Planter planter_prefab;
    private Tile parent = null;
    public bool walkable = true;
    public float f_cost { get { return h_cost + g_cost; } }
    public float h_cost;
    public float g_cost = Mathf.Infinity;
    public Tile GetParent() { return parent; }
    public void SetParent(Tile p)  {parent = p;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        if (ServiceLocator.GetPlayer().GetPlacement() && !EventSystem.current.IsPointerOverGameObject()) 
        {
            Instantiate(planter_prefab, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
            int money = ServiceLocator.GetPlayer().SpendMoney(20);
            if (money < 20) 
            {
                ServiceLocator.GetPlayer().TogglePlacement();
            }
        }
        
    }

}
