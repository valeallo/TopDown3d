using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerUpHandler
{
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
}
