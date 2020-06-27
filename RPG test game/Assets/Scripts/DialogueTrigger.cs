using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private Dialogue _dialogue;
    private DialogueManager _dialogueManager;
    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Interact()
    {
        _dialogueManager.StartDialogue(_dialogue);
    }
}
