using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler
{
    private InventoryItem item_in_slot;
    private Vector2 canvas_position;

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
        canvas_position = GetComponent<RectTransform>().anchoredPosition;
        Transform t = transform.parent;
        while (!t.GetComponent<Canvas>()) 
        {
            canvas_position += t.GetComponent<RectTransform>().anchoredPosition;
            t = t.parent;
        }
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
           
            Debug.Log(Vector2.Distance(ServiceLocator.GetPlayer().held_item.GetComponent<RectTransform>().anchoredPosition, canvas_position));
            if (Vector2.Distance(ServiceLocator.GetPlayer().held_item.GetComponent<RectTransform>().anchoredPosition, canvas_position) < 40)
            {
                Debug.Log("item in slot");
                item_in_slot.SetSlot(ServiceLocator.GetPlayer().held_item.GetSlot());
                ServiceLocator.GetPlayer().held_item.SetSlot(this);
                ServiceLocator.GetPlayer().held_item = null;
            }
        
        }
    }

    public void SetItem(InventoryItem item) 
    {
        item_in_slot = item;
    }
}
