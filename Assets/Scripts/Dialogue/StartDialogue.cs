using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogueGraph dialogueGraph;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.dialogue.SetActive(true);

            GameManager.instance.dialogueSystem.InitGraph(dialogueGraph);
        }
    }
}
