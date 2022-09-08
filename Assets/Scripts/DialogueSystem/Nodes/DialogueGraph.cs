using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph {

	public BaseNode currentNode;
	public BaseNode startNode; 

	public void Start()
    {
		currentNode = startNode;
		Execute();
    }

	public void Execute()
    {
		currentNode.Execute(); 
    }

}