using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[Serializable]
[NodeWidth(290)]
public class DialogueNode : BaseNode {


	public Sprite character = null;
	public string speaker = "";
	[TextArea(3, 10)]
	public string dialogue = "";

	public bool changeColor = false;
	public Color color = Color.clear;

	public bool dialogueOptions = false;

	private GameManagerAbril instance;

	[System.Serializable]
	public class DialogueOption
    {
		public string dialogue;
		public string option; 

		public DialogueOption(string _dialogue, string _option)
        {
			dialogue = _dialogue;
			option = _option; 
        }
    }

	[SerializeField]
	public List<DialogueOption> dialogueOptionList = new List<DialogueOption>(); 

	// Use this for initialization
	protected override void Init() {
		base.Init();
	}

	public override System.Type ReturnType()
	{
		return typeof(DialogueNode);
	}

	public override void Execute()
    {
        if (instance == null) instance = GameManagerAbril.instance;

        instance.dialogueSystem.DisplayDialogue(speaker, dialogue, character);

		instance.dialogueSystem.ExecuteAnimation();

        if (changeColor)
		{
            instance.dialogueSystem.SetBoxColor(color);
		}
        instance.dialogueSystem.displayOptions = dialogueOptions;
    }
}