
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField]
    private DialogueNode _firstNode;
    public DialogueNode FirstNode => _firstNode;
}
