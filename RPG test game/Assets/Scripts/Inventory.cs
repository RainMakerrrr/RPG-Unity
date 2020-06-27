using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _maxCountOfInventory = 10;
    public List<Item> Items = new List<Item>();
   
    public event UnityAction OnItemChanged;
    
    
    public static Inventory Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
    }

   
    
    
    public bool AddItems(Item item)
    {
        if (!item.isDefaultItem )
        {
            if (Items.Count >= _maxCountOfInventory) return false;

            Items.Add(item);
       
            OnItemChanged?.Invoke();
            
        }
        
        return true;
       
    }
    public void RemoveItems(Item item)
    {
        Items.Remove(item);
        OnItemChanged?.Invoke();
    }

    
}
