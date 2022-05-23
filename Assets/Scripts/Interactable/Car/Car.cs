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

    public override string OnHover()
    {
        if (Inventory.Instance.Items.Count == 0)
        {
            return "Can't leave until you get a present (of sorts)";
        }
        if (Inventory.Instance.paidForItems)
        {
            return "Left Click to go home";
        }
        return "Left Click to steal everything and tear off into the night";
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
