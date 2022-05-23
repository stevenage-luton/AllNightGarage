using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private DialogueInteractionScript _dialogueInteractionScript;

    public Transform target;

    [SerializeField]
    public Transform headTransform;

    public float speed;

    public float minimumX;
    public float maximumX;

    public float minimumY;
    public float maximumY;

    public float rotationAmount;

    [SerializeField]
    private Dialogue _defaultDialogue;
    [SerializeField]
    private Dialogue _repeatDialogueNoBasket;
    [SerializeField]
    private Dialogue _repeatDialogueBasket;
    [SerializeField]
    private Dialogue _repeatDialogueStillNoBasket;
    [SerializeField]
    private Dialogue _finalDialogue;

    private Dialogue _dialogueToPlay;

    [SerializeField]
    DialogueChannel _dialogueChannel;

    private DialogueSequencer _DialogueSequencer;

    private void Awake()
    {
        _DialogueSequencer = new DialogueSequencer();

        _DialogueSequencer.OnDialogueStart += OnDialogueStart;
        _DialogueSequencer.OnDialogueEnd += OnDialogueEnd;

        _dialogueChannel.OnDialogueRequested += _DialogueSequencer.StartDialogue;
        _dialogueChannel.OnDialogueNodeRequested += _DialogueSequencer.StartDialogueNode;

        _dialogueToPlay = _defaultDialogue;
    }

    private void OnDestroy()
    {
        _dialogueChannel.OnDialogueNodeRequested -= _DialogueSequencer.StartDialogueNode;
        _dialogueChannel.OnDialogueRequested -= _DialogueSequencer.StartDialogue;

        _DialogueSequencer.OnDialogueEnd -= OnDialogueEnd;
        _DialogueSequencer.OnDialogueStart -= OnDialogueStart;

        _DialogueSequencer = null;
    }

    void Start()
    {
        _dialogueInteractionScript.setDialogue(_dialogueToPlay);
        _dialogueInteractionScript.canPlay = true;
        _dialogueInteractionScript.SetHoverString("Left Click to talk to the cashier");
    }

    void Update()
    {
        if (Inventory.Instance.paidForItems)
        {
            _dialogueToPlay = _repeatDialogueBasket;
            _dialogueInteractionScript.setDialogue(_finalDialogue);
            return;
        }
        if (_dialogueToPlay == _defaultDialogue)
        {
            return;
        }    
        if (Inventory.Instance.hasBasket)
        {
            _dialogueToPlay = _repeatDialogueBasket;
            _dialogueInteractionScript.setDialogue(_dialogueToPlay);
        }
    }

    void LateUpdate()
    {
        Quaternion OriginalRot = headTransform.rotation;
        headTransform.LookAt(target);
        Quaternion NewRot = transform.rotation;
        //headTransform.rotation = OriginalRot;

        float ry = headTransform.eulerAngles.y;
        if (ry >= 180)
        {
            ry -= 360;
        }
        headTransform.eulerAngles = new Vector3(
            Mathf.Clamp(headTransform.eulerAngles.x, minimumX, maximumX),
            Mathf.Clamp(ry, minimumY, maximumY),
            0
        );
    }

    private void OnDialogueStart(Dialogue dialogue)
    {
        _animator.SetBool("Talking", true);
    }

    private void OnDialogueEnd(Dialogue dialogue)
    {
        _animator.SetBool("Talking", false);

        if (dialogue == _defaultDialogue)
        {
            _dialogueToPlay = _repeatDialogueNoBasket;
            _dialogueInteractionScript.setDialogue(_dialogueToPlay);
        }
        if (dialogue == _repeatDialogueNoBasket)
        {
            _dialogueToPlay = _repeatDialogueStillNoBasket;
            _dialogueInteractionScript.setDialogue(_dialogueToPlay);
        }
    }
}
