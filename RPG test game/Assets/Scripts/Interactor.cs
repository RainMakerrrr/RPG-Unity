using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]private IInteractable _interactable;

    void Start()
    {
        
    }

    
    private void Update()
    {
        CheckForInteraction();
    }
    
    private void CheckForInteraction()
    {
        if (_interactable == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            _interactable.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;

        _interactable = interactable;
    }
    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        if (interactable != _interactable) return;

        _interactable = null;
    }
}
