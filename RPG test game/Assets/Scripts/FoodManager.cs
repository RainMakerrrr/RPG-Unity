using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public List<GameObject> Food = new List<GameObject>();
    
    public static FoodManager Instance;
    private void Awake()
    {
        Instance = this;
    }

}
