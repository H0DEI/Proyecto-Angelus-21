using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAbril : MonoBehaviour
{
    public static GameManagerAbril instance;

    public DialogueSystem dialogueSystem;

    public GameObject characters;

    public GameObject dialogue;

    private void Awake()
    {
        instance = this;
    }
}
