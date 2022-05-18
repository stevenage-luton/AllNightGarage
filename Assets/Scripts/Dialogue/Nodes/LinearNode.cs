using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Dialogue/Node/Linear")]
public class LinearNode : DialogueNode
{
    [SerializeField]
    private DialogueNode _NextNode;
    public DialogueNode NextNode => _NextNode;


    public override bool CanBeFollowedByNode(DialogueNode node)
    {
        return _NextNode == node;
    }

    public override void Accept(NodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}
