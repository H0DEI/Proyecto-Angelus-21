using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{

    public static DialogueSystem dsInstance;

    private GameManagerAbril instance;

    private Animator animator;

    private Transform character;

    public void Start()
    {
        if (dsInstance == null)
        {
            dsInstance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    public CanvasGroup dialogueBox;
    public CanvasGroup dialogueCanvas;
    public CanvasGroup choicesCanvas;

    public Image characterIcon;
    public Image boxBackground;
    public TextMeshProUGUI speaker;
    public TextMeshProUGUI dialogue;

    public TextMeshProUGUI[] choices;

    public DialogueGraph currentGraph;

    public bool playing = false;
    public bool pickingOption = false;
    public bool displayOptions = false;

    public int currentSelection = 0;
    public int maxSelection = 0;

    public void DisplayDialogue(string _speaker, string _dialogue, Sprite _icon)
    {
        dialogueCanvas.alpha = 1f;
        choicesCanvas.alpha = 0f; 
        characterIcon.sprite = _icon;
        speaker.text = _speaker;
        dialogue.text = _dialogue;
    }

    public void InitGraph(DialogueGraph graph)
    {
        dialogueBox.alpha = 1f;
        currentGraph = graph;
        currentGraph.Start();
        playing = true;
    }

    public void Stop()
    {
        dialogueBox.alpha = 0f;
        playing = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && playing && !pickingOption)
        {
            if (displayOptions)
            {
                DisplayOptions(currentGraph.currentNode as DialogueNode);
                pickingOption = true;
                return;
            }
            currentGraph.currentNode = currentGraph.currentNode.NextNode("exit");

            currentGraph.currentNode.Execute();
        }
        else if (playing && pickingOption)
        {
            bool up = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
            bool enter = Input.GetKeyDown(KeyCode.Return); 

            if (up)
            {
                UpdateSelection(currentSelection - 1);
            }
            if (down)
            {
                UpdateSelection(currentSelection + 1);
            }
            if (enter)
            {
                Debug.Log("Entering");
                PickOption(); 
            }
        }
    }

    public void DisplayOptions(DialogueNode _node)
    {
        choicesCanvas.alpha = 1f;
        dialogueCanvas.alpha = 0f; 
        int index = 0;
        foreach (DialogueNode.DialogueOption d in _node.dialogueOptionList)
        {
            maxSelection = index; 
            choices[index].text = d.dialogue;
            index++;
            if (index == 3)
            {
                break;
            }
        }
        UpdateSelection(0);
    }

    public void UpdateSelection(int _selection)
    {
        currentSelection = _selection;
        if (currentSelection < 0)
        {
            currentSelection = maxSelection;
        }
        if (currentSelection > maxSelection)
        {
            currentSelection = 0; 
        }
        for (int i = 0; i <= 2; i++)
        {
            if (i <= maxSelection)
            {
                choices[i].color = Color.black;
            }
            else
            {
                choices[i].color = Color.clear; 
            }
        }
        choices[currentSelection].color = Color.white;
    }

    public void PickOption()
    {
        pickingOption = false;
        choicesCanvas.alpha = 0f;
        dialogueCanvas.alpha = 1f; 

        DialogueNode targetNode = currentGraph.currentNode as DialogueNode; 

        currentGraph.currentNode = currentGraph.currentNode.NextNode(targetNode.dialogueOptionList[currentSelection].option );
        currentGraph.currentNode.Execute();
    }

    public void SetBoxColor(Color _color)
    {
        boxBackground.color = _color;
    }

    public void ExecuteAnimation()
    {
        if (instance == null) instance = GameManagerAbril.instance;

        if (!(currentGraph.currentNode.animations == null))
        {
            foreach(TwoStrings characterAnimation in currentGraph.currentNode.animations)
            {
                character = instance.characters.transform.Find(characterAnimation.name);

                if (character != null)
                {
                    animator = character.GetComponent<Animator>();

                    animator.SetTrigger(characterAnimation.value);
                }
            }
        }

        character = null;
    }
}
