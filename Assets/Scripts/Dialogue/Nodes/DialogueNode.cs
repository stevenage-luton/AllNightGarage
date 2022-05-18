using UnityEngine;

public abstract class DialogueNode : ScriptableObject
{
    [SerializeField]
    private DialogueLine _dialogueline;

    public DialogueLine Line => _dialogueline;

    public abstract bool CanBeFollowedByNode(DialogueNode node);

    public abstract void Accept(NodeVisitor visitor);
}
