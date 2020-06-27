using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{ 
	[SerializeField] private float _baseValue;
	public List<float> Modifiers = new List<float>();

	public float GetValue()
	{
		return _baseValue;
	}

	public void AddModifier(float modifier)
	{
		if (modifier != 0)
			Modifiers.Add(modifier);
	}
	public void RemoveModifier(float modifier)
	{
		if (modifier != 0)
			Modifiers.Remove(modifier);
	}
	
}
