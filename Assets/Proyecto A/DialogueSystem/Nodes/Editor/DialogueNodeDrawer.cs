using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
using System.Linq;

[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeDrawer : NodeEditor
{

    private DialogueNode dialogueNode;

    private bool showEntryNode = true;

    private bool showNodeSettings = false;

    private bool showDialogueSettings = false;

    private string newDialogueOption = "";
    private string newDialogueOptionOutput = "";

    private int currentNodeTab = 0;

    private int nodePortToDelete = 0;

    public override void OnBodyGUI()
    {
        if (dialogueNode == null)
        {
            dialogueNode = target as DialogueNode;
        }

        serializedObject.Update();

        if (showEntryNode)
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));
        }

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));

        Color prev = GUI.backgroundColor;
        GUI.backgroundColor = Color.red;

        if (GUILayout.Button("Toggle entry node"))
        {
            showEntryNode = !showEntryNode;
        }

        GUI.backgroundColor = prev;

        showNodeSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showNodeSettings, "Node Settings");

        if (showNodeSettings)
        {
            currentNodeTab = GUILayout.Toolbar(currentNodeTab, new string[] { "Add Output", "Remove Output" });
            switch (currentNodeTab)
            {
                case 0:
                    EditorGUILayout.PrefixLabel("Dialogue");
                    newDialogueOption = EditorGUILayout.TextField(newDialogueOption);
                    EditorGUILayout.PrefixLabel("Output");
                    newDialogueOptionOutput = EditorGUILayout.TextField(newDialogueOptionOutput);
                    if (GUILayout.Button("Create new option"))
                    {
                        bool noDialogue = (newDialogueOption.Length == 0);
                        bool noOption = (newDialogueOptionOutput.Length == 0);
                        bool matchesExistingOutput = false;

                        foreach (NodePort p in dialogueNode.DynamicOutputs)
                        {
                            if (p.fieldName == newDialogueOptionOutput)
                            {
                                matchesExistingOutput = true;
                                break;
                            }
                        }

                        if (noDialogue)
                        {
                            EditorUtility.DisplayDialog("Error creating port", "you will not be seen alive again.", "OK");
                            return;
                        }
                        if (noOption)
                        {
                            EditorUtility.DisplayDialog("Error creating port", "No output port was specified.", "OK");
                            return;
                        }
                        if (matchesExistingOutput)
                        {
                            EditorUtility.DisplayDialog("Error creating port", "The requested port is already in use.", "OK");
                            return;
                        }

                        //If we got here, it means that we can run the requested process.
                        dialogueNode.AddDynamicOutput(typeof(int), Node.ConnectionType.Multiple, Node.TypeConstraint.None, newDialogueOptionOutput);
                        dialogueNode.dialogueOptionList.Add(new DialogueNode.DialogueOption(newDialogueOption, newDialogueOptionOutput));
                    }
                    break;
                case 1:

                    if (dialogueNode.DynamicOutputs.Count() == 0)
                    {
                        EditorGUILayout.HelpBox("HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP HELP", MessageType.Error);
                    }

                    else
                    {
                        EditorGUILayout.PrefixLabel("Choose Port");

                        List<string> outputs = new List<string>();
                        foreach (NodePort p in dialogueNode.DynamicOutputs)
                        {
                            outputs.Add(p.fieldName);
                        }

                        nodePortToDelete = EditorGUILayout.Popup(nodePortToDelete, outputs.ToArray());

                        if (GUILayout.Button("Delete selected node"))
                        {
                            foreach (DialogueNode.DialogueOption d in dialogueNode.dialogueOptionList)
                            {
                                if (d.option == dialogueNode.DynamicOutputs.ElementAt(nodePortToDelete).fieldName)
                                {
                                    dialogueNode.dialogueOptionList.Remove(d);
                                    break;
                                }
                            }

                            dialogueNode.RemoveDynamicPort(dialogueNode.DynamicOutputs.ElementAt(nodePortToDelete));
                        }
                    }
                    break;
            }
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        showDialogueSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showDialogueSettings, "Dialogue Settings");

        if (showDialogueSettings)
        {
            float prevWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 150;
            dialogueNode.character = (Sprite)EditorGUILayout.ObjectField("Character Sprite", dialogueNode.character, typeof(Sprite), false);

            EditorGUIUtility.labelWidth = prevWidth;

            dialogueNode.speaker = EditorGUILayout.TextField("Speaker", dialogueNode.speaker);

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("dialogue"));

            EditorGUIUtility.labelWidth = 150;
            dialogueNode.dialogueOptions = EditorGUILayout.Toggle("Show dialogue options?", dialogueNode.dialogueOptions);

            EditorGUIUtility.labelWidth = prevWidth;

            if (dialogueNode.dialogueOptions)
            {
                foreach (DialogueNode.DialogueOption d in dialogueNode.dialogueOptionList)
                {
                    EditorGUILayout.PrefixLabel(d.dialogue);
                    d.dialogue = EditorGUILayout.TextField(d.dialogue);
                    EditorGUILayout.TextField(d.option);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                }
                foreach (NodePort p in dialogueNode.DynamicOutputs)
                {
                    NodeEditorGUILayout.PortField(p);
                }
            }
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        //if (showEntryNode)
        //{
        //    NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));
        //}

        //if (GUILayout.Button(showEntryNode ? "Hide Entry Node" : "Show Entry Node"))
        //{
        //    showEntryNode = !showEntryNode;
        //}

        //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));

        //displayingDialogueInfo = EditorGUILayout.BeginFoldoutHeaderGroup(displayingDialogueInfo, "Dialogue Settings");we w

        //if (displayingDialogueInfo)
        //{
        //    dialogueNode.character = (Sprite)EditorGUILayout.ObjectField("Character Sprite", dialogueNode.character, typeof(Sprite), true);
        //}

        //EditorGUILayout.EndFoldoutHeaderGroup(); 
    }
}
