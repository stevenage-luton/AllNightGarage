using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Interactable
{
    [SerializeField]
    private CutsceneManager _cutsceneManager;
    public override void OnClick()
    {
        Debug.Log("clicked on car!");
        CanEndCheck();
    }

    private void CanEndCheck()
    {
        if (Inventory.Instance.Items.Count == 0)
        {
            Debug.Log("clicked on car but no items!");
            return;
        }


        _cutsceneManager.BeginOutro(!Inventory.Instance.paidForItems);
        
    }
}
