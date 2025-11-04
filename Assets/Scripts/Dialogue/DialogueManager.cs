using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    [SerializeField] private float typeSpeed = 0.05f;

    private DialogueContainer currentDialogue;
    private int currentLineIndex;
    private bool isTyping;
    
    private Coroutine typingCoroutine;
    
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

    private void DisplayCurrentLine()
    {
        // stop the old typing if it exists
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    
        speakerText.text = currentDialogue.dialogueSet[currentLineIndex].speakerName;
        string textToShow = currentDialogue.dialogueSet[currentLineIndex].dialogueLine;
    
        // start and store typing coroutine
        typingCoroutine = StartCoroutine(TypeText(textToShow));
    }
    
    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueText.text = ""; 
    
        // loop goes through each character
        foreach (char letter in textToType)
        {
            dialogueText.text += letter;
            // play text audio here (single click)
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    private void NextLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue.dialogueSet[currentLineIndex].dialogueLine;
            isTyping = false;
            return;
        }
        if (currentLineIndex+1 < currentDialogue.dialogueSet.Length && !isTyping)
        {
            currentLineIndex++;
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        speakerText.text = "";
    }
}
