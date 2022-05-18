using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    [SerializeField]
    private DialogueCharacter _dialogueCharacter;
    [SerializeField]
    private string _dialogueText;

    public DialogueCharacter Speaker => _dialogueCharacter;
    public string DialogueText => _dialogueText;
}
