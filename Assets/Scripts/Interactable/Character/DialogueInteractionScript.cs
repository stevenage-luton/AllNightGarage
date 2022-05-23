using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractionScript : Interactable
{

    UnityEvent _onInteract = new UnityEvent();

    [SerializeField]
    DialogueChannel _dialogueChannel;

    private DialogueSequencer _DialogueSequencer;

    private string _hoverText;

    public bool canPlay;

    [SerializeField]
    private Dialogue _dialogueToPlay;

    void Awake()
    {

        canPlay = false;
        _onInteract.AddListener(BeginDialogue);
    }

    public override void OnClick()
    {
        //Debug.Log("OnClickEvent for character playing!");
        if (canPlay)
        {
            _onInteract.Invoke();
        }     
    }
    public override string OnHover()
    {
        return _hoverText;
    }

    void BeginDialogue()
    {
        _dialogueChannel.RaiseRequestDialogue(_dialogueToPlay);
    }

    public void setDialogue(Dialogue dialogue)
    {
        _dialogueToPlay = dialogue;
    }

    public void SetHoverString(string textToUse)
    {
        _hoverText = textToUse;
    }
}
