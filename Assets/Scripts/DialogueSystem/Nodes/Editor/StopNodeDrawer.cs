using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(StopNode))]
public class StopNodeDrawer : NodeEditor
{

    private StopNode stopNode;

    private bool showExitNode = false;

    private static readonly Color backgroundColor = new Color(0.0f, 0.0f, 1f, 1f);

    public override void OnBodyGUI()
    {

        if (stopNode == null)
        {
            stopNode = target as StopNode;
        }

        serializedObject.Update();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));

        if (showExitNode)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));
        }

        if (GUILayout.Button(showExitNode ? "Hide Exit Node" : "Show Exit Node"))
        {
            showExitNode = !showExitNode;
        }


    }
}
