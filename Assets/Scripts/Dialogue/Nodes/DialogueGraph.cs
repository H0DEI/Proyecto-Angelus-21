using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph {

	public BaseNode currentNode;
	public BaseNode startNode;

    private GameManager instance;


    public void Start()
    {
        if (instance == null) instance = GameManager.instance;

        currentNode = startNode;

		Execute();
    }

	public void Execute()
    {
		currentNode.Execute();
    }
}