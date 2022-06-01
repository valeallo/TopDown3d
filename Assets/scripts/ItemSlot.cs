using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler
{
    private InventoryItem item_in_slot;

    public void OnPointerUp(PointerEventData eventData)
    {
        if (ServiceLocator.GetPlayer().held_item != null) 
        { 
        ServiceLocator.GetPlayer().held_item.SetSlot(this);
        ServiceLocator.GetPlayer().held_item = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        if (ServiceLocator.GetPlayer().held_item != null && !Input.GetMouseButton(0))
        {
            Debug.Log("item released");
            Vector2 slot_position = ServiceLocator.GetPlayer().held_item.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("item position:" + slot_position);
          
            if (Vector2.Distance(ServiceLocator.GetPlayer().held_item.transform.position, transform.position) < 40)
            {
                Debug.Log("item in slot");
                item_in_slot.SetSlot(ServiceLocator.GetPlayer().held_item.GetSlot());
                ServiceLocator.GetPlayer().held_item.SetSlot(this);
         
            }
        
        }
    }

    public void SetItem(InventoryItem item) 
    {
        item_in_slot = item;
    }
}
