using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Dialogue/DialogueChannel")]
public class DialogueChannel : ScriptableObject
{
    public delegate void DialogueCallback(Dialogue dialogue);
    //requests the start of a dialogue
    public DialogueCallback OnDialogueRequested;
    //These two events fire when dialogue begins and ends, for the UI updating
    public DialogueCallback OnDialogueStart;
    public DialogueCallback OnDialogueEnd;

    public delegate void DialogueNodeCallback(DialogueNode node);
    //requests the start of a dialogue node
    public DialogueNodeCallback OnDialogueNodeRequested;
    //These two events fire when a node begins and ends, for the UI updating
    public DialogueNodeCallback OnDialogueNodeStart;
    public DialogueNodeCallback OnDialogueNodeEnd;

    public void RaiseRequestDialogue(Dialogue dialogue)
    {
        OnDialogueRequested?.Invoke(dialogue);
    }

    public void RaiseDialogueStart(Dialogue dialogue)
    {
        Debug.Log("Starting Dialogue");
        OnDialogueStart?.Invoke(dialogue);
    }

    public void RaiseDialogueEnd(Dialogue dialogue)
    {
        OnDialogueEnd?.Invoke(dialogue);
    }

    public void RaiseRequestDialogueNode(DialogueNode node)
    {
        OnDialogueNodeRequested?.Invoke(node);
    }

    public void RaiseDialogueNodeStart(DialogueNode node)
    {
        OnDialogueNodeStart?.Invoke(node);
    }

    public void RaiseDialogueNodeEnd(DialogueNode node)
    {
        OnDialogueNodeEnd?.Invoke(node);
    }
}