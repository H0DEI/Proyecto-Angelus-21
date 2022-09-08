using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(DialogueGraph))]
public class DialogueGraphDrawer : NodeGraphEditor
{

    private DialogueGraph dialogueGraph;

    private Node lastNode; 

    public override void OnGUI()
    {
        base.OnGUI();

        if (dialogueGraph == null)
        {
            dialogueGraph = target as DialogueGraph;
        }

        serializedObject.Update(); 

        Color backgroundColor = new Color(25, 25, 25, 0.8f);

        GUI.backgroundColor = backgroundColor;
        GUILayout.BeginVertical("Menu", "window", GUILayout.Width(200), GUILayout.Height(200));

        GUI.backgroundColor = Color.grey;

        Node targetedNode = (Node)Selection.activeObject;

        if (targetedNode != null)
        {
            lastNode = targetedNode; 
        }

        if (GUILayout.Button("Center last node"))
        {
            if (lastNode != null)
            {
                window.zoom = 1; 
             
                float flippedX = lastNode.position.x >= 0 ? lastNode.position.x * -1 : Mathf.Abs(lastNode.position.x);
                float flippedY = lastNode.position.y >= 0 ? lastNode.position.y * -1 : Mathf.Abs(lastNode.position.y);
                window.panOffset = new Vector2(flippedX, flippedY) - new Vector2(150, 0);
            }
            else
            {
                Debug.LogWarning("No node selected.");
            }
        }

        GUILayout.EndVertical(); 
    }

}
