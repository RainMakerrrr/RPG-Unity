using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _item;
    
   
    public void Interact()
    {
        PickingUpItem();
        
    }

    private void PickingUpItem()
    {
        Debug.Log("Item was pickedup" + _item.ItemName);
        bool wasPickedUp =  Inventory.Instance.AddItems(_item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
       
    }

}
