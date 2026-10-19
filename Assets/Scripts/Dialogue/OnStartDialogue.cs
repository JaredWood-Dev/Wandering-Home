using System;
using UnityEngine;

public class OnStartDialogue : MonoBehaviour
{
    public Dialogue startDialogue;
    public DialogueManager manager;

    private void Start()
    {
        manager.StartDialogue(startDialogue);
    }
}
