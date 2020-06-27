using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="New Food",menuName ="Inventory/Food")]
public class HealingItem : Item
{
	//public float HealthUnits;
	//public float StaminaUnits;
	//public float DamageUnits;

	public override void Use()
	{
		Debug.Log("Use " + name + "item");
		//RemoveFromInventory();
	}
}
