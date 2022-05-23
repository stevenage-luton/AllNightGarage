using UnityEngine;

public class DialogueInstigator : MonoBehaviour
{
    [SerializeField]
    private DialogueChannel _DialogueChannel;

    private DialogueSequencer _DialogueSequencer;

    [SerializeField]
    private PlayerMovement _playermovement;

    private void Awake()
    {
        _DialogueSequencer = new DialogueSequencer();

        _DialogueSequencer.OnDialogueStart += OnDialogueStart;
        _DialogueSequencer.OnDialogueEnd += OnDialogueEnd;
        _DialogueSequencer.OnDialogueNodeStart += _DialogueChannel.RaiseDialogueNodeStart;
        _DialogueSequencer.OnDialogueNodeEnd += _DialogueChannel.RaiseDialogueNodeEnd;

        _DialogueChannel.OnDialogueRequested += _DialogueSequencer.StartDialogue;
        _DialogueChannel.OnDialogueNodeRequested += _DialogueSequencer.StartDialogueNode;
    }

    private void OnDestroy()
    {
        _DialogueChannel.OnDialogueNodeRequested -= _DialogueSequencer.StartDialogueNode;
        _DialogueChannel.OnDialogueRequested -= _DialogueSequencer.StartDialogue;

        _DialogueSequencer.OnDialogueNodeEnd -= _DialogueChannel.RaiseDialogueNodeEnd;
        _DialogueSequencer.OnDialogueNodeStart -= _DialogueChannel.RaiseDialogueNodeStart;
        _DialogueSequencer.OnDialogueEnd -= OnDialogueEnd;
        _DialogueSequencer.OnDialogueStart -= OnDialogueStart;

        _DialogueSequencer = null;
    }

    private void OnDialogueStart(Dialogue dialogue)
    {
        _DialogueChannel.RaiseDialogueStart(dialogue);
        MouseLook mouseLook = gameObject.GetComponentInChildren<MouseLook>();
        mouseLook.state = MouseLook.State.Dialogue;
        UIScript.ChangeUI(UIEnum.DIALOGUE);
        _playermovement.remainStationary = true;
    }

    private void OnDialogueEnd(Dialogue dialogue)
    {
        _DialogueChannel.RaiseDialogueEnd(dialogue);
        gameObject.GetComponentInChildren<MouseLook>().state = MouseLook.State.Movement;
        UIScript.ChangeUI(UIEnum.CROSSHAIR);
        _playermovement.remainStationary = false;
    }
}