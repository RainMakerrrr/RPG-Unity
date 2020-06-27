using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName ="new Item",menuName ="Item/Inventory")]
public class Item : ScriptableObject
{
   

    public string ItemName = "New Item";
    public Sprite ItemIcon = null;
    public bool isDefaultItem = false;

    
    public float HealthUnits;
    public float StaminaUnits;
    public float DamageUnits;

   
    public virtual void Use()
    {
        
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.RemoveItems(this);
    }
}
