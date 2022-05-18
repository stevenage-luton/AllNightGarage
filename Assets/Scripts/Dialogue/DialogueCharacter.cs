
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Dialogue/Character")]
public class DialogueCharacter: ScriptableObject
{
    [SerializeField]
    private string _characterName;

    public string CharacterName => _characterName;
}