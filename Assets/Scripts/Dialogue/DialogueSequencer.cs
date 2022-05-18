using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequencer
{
    public delegate void DialogueCallback(Dialogue dialogue);
    public delegate void DialogueNodeCallback(DialogueNode node);

    public DialogueCallback OnDialogueStart;
    public DialogueCallback OnDialogueEnd;
    public DialogueNodeCallback OnDialogueNodeStart;
    public DialogueNodeCallback OnDialogueNodeEnd;

    private Dialogue _CurrentDialogue;
    private DialogueNode _CurrentNode;

    /// <summary>
    /// Watches the DialogueChannel for the Start Dialogue event. Starts a given piece of dialogue, with the first node.
    /// </summary>
    /// <param name="dialogue">the dialogue to start</param>
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("starting dialogue from sequence");
        _CurrentDialogue = dialogue;
        OnDialogueStart?.Invoke(_CurrentDialogue);
        StartDialogueNode(dialogue.FirstNode);


    }

    public void EndDialogue(Dialogue dialogue)
    {

        StopDialogueNode(_CurrentNode);
        OnDialogueEnd?.Invoke(_CurrentDialogue);
        _CurrentDialogue = null;


    }
    /// <summary>
    /// watches the DialogueChannel for the NodeStart event. starts an individual dialogue node.
    /// </summary>
    /// <param name="node">the node to start</param>
    public void StartDialogueNode(DialogueNode node)
    {

        StopDialogueNode(_CurrentNode);

        _CurrentNode = node;

        if (_CurrentNode != null)
        {
            OnDialogueNodeStart?.Invoke(_CurrentNode);
        }
        else
        {
            EndDialogue(_CurrentDialogue);
        }


    }

    private void StopDialogueNode(DialogueNode node)
    {

        OnDialogueNodeEnd?.Invoke(_CurrentNode);
        _CurrentNode = null;

    }
}