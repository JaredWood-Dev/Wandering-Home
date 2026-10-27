using System;
using System.Collections;
using UnityEngine;

public class Epilogue : MonoBehaviour
{
    public Fade overlay;
    public Dialogue epilogueDialogue;
    public DialogueManager dialogueManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EpilogueSequence());
        }
    }

    IEnumerator EpilogueSequence()
    {
        StartCoroutine(overlay.DoFade(0, 1, 1));
        yield return new WaitForSeconds(1);
        dialogueManager.StartDialogue(epilogueDialogue);
    }
}
