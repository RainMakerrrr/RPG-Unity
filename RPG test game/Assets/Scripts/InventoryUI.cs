using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    
    [SerializeField] private Transform _inventoryParentTransform;
    [SerializeField] private GameObject _inventoryUI;
    private InventorySlot[] _slots;
   
    private void Start()
    {
        Inventory.Instance.OnItemChanged += OnChangeUI;
        _slots = _inventoryParentTransform.GetComponentsInChildren<InventorySlot>();
        
    }

    
    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        }
    }

    private void OnChangeUI()
    {
        for(int i = 0; i < _slots.Length; i++)
        {
            if(i < Inventory.Instance.Items.Count)
            {
                _slots[i].AddSlot(Inventory.Instance.Items[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}
