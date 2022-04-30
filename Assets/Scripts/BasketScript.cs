using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : ItemScript
{
    public override void OnPickup()
    {
        Inventory.Instance.TakeBasket();
        UIScript.TakeBasket();
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.PickupItem(m_ItemDataSO);
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
