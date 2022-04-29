using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : ItemScript
{
    public new void OnPickup()
    {
        Inventory.Instance.TakeBasket();
        UIScript.TakeBasket();
    }
}
