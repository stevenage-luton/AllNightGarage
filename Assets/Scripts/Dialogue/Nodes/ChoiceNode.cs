using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
    [SerializeField]
    private string _ChoicePreview;
    [SerializeField]
    private DialogueNode _ChoiceNode;

    public string ChoicePreview => _ChoicePreview;
    public DialogueNode ChoiceNode => _ChoiceNode;
}




[CreateAssetMenu(menuName = "ScriptableObject/Dialogue/Node/Choice")]
public class ChoiceNode : DialogueNode
{
    [SerializeField]
    private DialogueChoice[] _Choices;
    public DialogueChoice[] Choices => _Choices;


    public override bool CanBeFollowedByNode(DialogueNode node)
    {
        return _Choices.Any(x => x.ChoiceNode == node);
    }

    public override void Accept(NodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}
