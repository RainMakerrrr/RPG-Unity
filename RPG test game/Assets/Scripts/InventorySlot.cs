using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _removeButton;
    [SerializeField] private CharacterStats _playerStats;
   
    
    private Item _item;

    private void Start()
    {
        _playerStats = FindObjectOfType<CharacterStats>();
       
    }
    public void AddSlot(Item item)
    { 
        _item = item;
        _icon.sprite = _item.ItemIcon;
        _icon.enabled = true;
        _removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
        _removeButton.interactable = false;
    }

    public void OnRemoveButtonClick()
    {
        Inventory.Instance.RemoveItems(_item);       
    }

    public void UseItem()
    {
        _playerStats.TakeHeal(_item.HealthUnits, _item.StaminaUnits, _item.DamageUnits);
        _item.RemoveFromInventory();
    }
}
