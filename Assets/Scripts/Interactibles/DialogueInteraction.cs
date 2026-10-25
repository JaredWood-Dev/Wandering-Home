using UnityEngine;

public class DialogueInteraction : MonoBehaviour, IInteractible
{
    public Dialogue text;
    public DialogueManager dialogueManager;
    
    public void Interact()
    {
        dialogueManager.StartDialogue(text);
    }
}
