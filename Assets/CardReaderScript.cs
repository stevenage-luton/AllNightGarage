using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReaderScript : MonoBehaviour    
{
    [SerializeField]
    private AudioSource _source;
    [SerializeField]
    private AudioClip _sfxBeep;
    [SerializeField]
    private Transform _heldBasketParent;
    private BasketScript _heldBasket;
    [SerializeField]
    private DialogueInteractionScript _dialogueInteractionScript;
    [SerializeField]
    private Dialogue _cardReaderDialogue;
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

    }

    private void OnDestroy()
    {
        _dialogueChannel.OnDialogueNodeRequested -= _DialogueSequencer.StartDialogueNode;
        _dialogueChannel.OnDialogueRequested -= _DialogueSequencer.StartDialogue;

        _DialogueSequencer.OnDialogueEnd -= OnDialogueEnd;
        _DialogueSequencer.OnDialogueStart -= OnDialogueStart;

        _DialogueSequencer = null;
    }

    private void Start()
    {     
        _dialogueInteractionScript.setDialogue(_cardReaderDialogue);
        _dialogueInteractionScript.SetHoverString("Can't pay if you don't have anything");
    }

    void Update()
    {
        if (Inventory.Instance.hasBasket)
        {
            _heldBasket = _heldBasketParent.GetComponentInChildren<BasketScript>();
            if (Inventory.Instance.Items.Count != 0)
            {
                _dialogueInteractionScript.SetHoverString("Left Click to talk pay");
                _dialogueInteractionScript.canPlay = true;
            }          
        }
    }


    void OnDialogueStart(Dialogue dialogue)
    {
        if (dialogue == _cardReaderDialogue)
        {
            _source.PlayOneShot(_sfxBeep);
            Inventory.Instance.paidForItems = true;
            _heldBasket.OnDrop();
        }
    }

    void OnDialogueEnd(Dialogue dialogue)
    {
        _dialogueInteractionScript.canPlay = false;
    }
}
