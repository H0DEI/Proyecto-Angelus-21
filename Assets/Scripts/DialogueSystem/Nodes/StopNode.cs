using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[System.Serializable]
[NodeTint("#ff5338")]
public class StopNode : BaseNode {

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	public override System.Type ReturnType()
	{
		return typeof(StopNode);
	}


	public override void Execute()
    {
		DialogueSystem.instance.Stop(); 
    }
}