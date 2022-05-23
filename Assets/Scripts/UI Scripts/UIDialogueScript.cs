using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueScript : MonoBehaviour, NodeVisitor
{
    [SerializeField]
    private TextMeshProUGUI _SpeakerText;
    [SerializeField]
    private TextMeshProUGUI _DialogueText;

    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip _sfxSpeach;

    [SerializeField]
    private RectTransform _ChoicesBoxTransform;

    public UIChoiceScript _UIChoiceScript;

    [SerializeField]
    public DialogueChannel _DialogueChannel;

    private bool _ListenToInput = true;
    private DialogueNode _NextNode = null;

    private WaitForSeconds _typeDelay = new WaitForSeconds(0.05f);

    private void Awake()
    {
        _DialogueChannel.OnDialogueNodeStart += OnDialogueNodeStart;
        _DialogueChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;

        gameObject.SetActive(false);
        _ChoicesBoxTransform.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _DialogueChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd;
        _DialogueChannel.OnDialogueNodeStart -= OnDialogueNodeStart;
    }

    private void Update()
    {
        if (_ListenToInput && Input.GetMouseButtonDown(0))
        {
            _DialogueChannel.RaiseRequestDialogueNode(_NextNode);
        }
    }

    private void OnDialogueNodeStart(DialogueNode node)
    {
        gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(TypeDialogueText(node.Line));
        _SpeakerText.text = node.Line.Speaker.CharacterName;

        node.Accept(this);
    }

    private void OnDialogueNodeEnd(DialogueNode node)
    {
        _NextNode = null;
        _ListenToInput = false;
        _DialogueText.text = "";
        _SpeakerText.text = "";

        foreach (Transform child in _ChoicesBoxTransform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
        _ChoicesBoxTransform.gameObject.SetActive(false);
    }

    public void Visit(LinearNode node)
    {
        _ListenToInput = true;
        _NextNode = node.NextNode;
    }

    public void Visit(ChoiceNode node)
    {
        _ChoicesBoxTransform.gameObject.SetActive(true);

        foreach (DialogueChoice choice in node.Choices)
        {
            UIChoiceScript newChoice = Instantiate(_UIChoiceScript, _ChoicesBoxTransform);
            newChoice.Choice = choice;
        }
    }

    IEnumerator TypeDialogueText(DialogueLine line)
    {
        _DialogueText.text = "";
        foreach (char letter in line.DialogueText.ToCharArray())
        {
            _DialogueText.text += letter;
            _source.PlayOneShot(_sfxSpeach);
            yield return _typeDelay;
        }
    }
}