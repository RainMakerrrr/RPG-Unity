using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private Animator _animator;
    private Queue<string> _sentences;
    private void Start()
    {
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _animator.SetBool("isOpen", true);
        _nameText.text = dialogue.Name;
        _sentences.Clear();

        foreach(var sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string nextSentece = _sentences.Dequeue();
        _dialogueText.text = nextSentece;
    }
    private void EndDialogue()
    {
        _animator.SetBool("isOpen", false);
    }
   
}
