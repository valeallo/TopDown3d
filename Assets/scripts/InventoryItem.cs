using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{ 
    private ItemSlot slot;
    public void OnPointerDown(PointerEventData eventData)
    {
        ServiceLocator.GetPlayer().held_item = this;
        transform.SetParent(transform.parent.parent.parent);
    }


    // Start is called before the first frame update
    void Start()
    {
        slot = transform.parent.GetComponent<ItemSlot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ServiceLocator.GetPlayer().held_item == this)
        {
            if (!Input.GetMouseButton(0))
            {
                transform.SetParent(slot.transform);
                transform.localPosition = new Vector3();
            }
        }
        
    }
    public void SetSlot(ItemSlot slot) 
    {
        this.slot = slot;      
    }
}
