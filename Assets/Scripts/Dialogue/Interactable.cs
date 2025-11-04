using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public DialogueContainer dialogueContainer;
    
    public void OnMouseDown()
    {
        print("Object clicked!");
        
        if (dialogueContainer != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueContainer);
        }
    }
}
