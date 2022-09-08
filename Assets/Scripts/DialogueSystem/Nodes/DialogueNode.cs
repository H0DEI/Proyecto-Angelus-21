using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


[NodeWidth(290)]
public class DialogueNode : BaseNode {


	public Sprite character = null;
	public string speaker = "";
	[TextArea(3, 10)]
	public string dialogue = "";

	public bool changeColor = false;
	public Color color = Color.clear;

	public bool dialogueOptions = false; 

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
		DialogueSystem.instance.DisplayDialogue(speaker, dialogue, character);
		if (changeColor)
		{
			DialogueSystem.instance.SetBoxColor(color);
		}
		DialogueSystem.instance.displayOptions = dialogueOptions;
    }
}