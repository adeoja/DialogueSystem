using System;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;

    private DialogueContainer currentDialogue;
    private int currentLineIndex;
    
   private void Awake()
   {
       if (Instance == null)
       {
           Instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
   }

   private void Update()
   {
       if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
       {
           NextLine();
       }
   }

   public void StartDialogue(DialogueContainer dialogueContainer)
    {
        dialoguePanel.SetActive(true);
        currentDialogue = dialogueContainer;
        currentLineIndex = 0;
        speakerText.text = currentDialogue.dialogueSet[0].speakerName;
        DisplayCurrentLine();
    }

    public void DisplayCurrentLine()
    {
        dialogueText.text = currentDialogue.dialogueSet[currentLineIndex].dialogueLine;
    }

    public void NextLine()
    {
        if (currentLineIndex+1 < currentDialogue.dialogueSet.Length)
        {
            currentLineIndex++;
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        speakerText.text = "";
    }
}
