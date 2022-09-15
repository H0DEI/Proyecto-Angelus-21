using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.SceneManagement;

[System.Serializable]
[NodeTint("#ff5338")]
public class StopNode : BaseNode {

	public string sceneLoad;

	protected override void Init() {
		base.Init();
		
	}

	public override System.Type ReturnType()
	{
		return typeof(StopNode);
	}


	public override void Execute()
    {
		DialogueSystem.dsInstance.Stop();

		if(sceneLoad != "") SceneManager.LoadScene(sceneLoad);
    }
}