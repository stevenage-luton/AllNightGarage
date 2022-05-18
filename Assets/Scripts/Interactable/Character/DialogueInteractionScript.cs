using UnityEngine;
using UnityEngine.Events;

public class DialogueInteractionScript : Interactable
{
    [SerializeField]
    UnityEvent _onInteract;

    public override void OnClick()
    {
        //Debug.Log("OnClickEvent for character playing!");
        _onInteract.Invoke();
    }
}
