using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIChoiceScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _Choice;
    [SerializeField]
    private DialogueChannel _DialogueChannel;

    private DialogueNode _ChoiceNextNode;

    public DialogueChoice Choice
    {
        set
        {
            _Choice.text = value.ChoicePreview;
            _ChoiceNextNode = value.ChoiceNode;
        }
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _DialogueChannel.RaiseRequestDialogueNode(_ChoiceNextNode);
    }
}