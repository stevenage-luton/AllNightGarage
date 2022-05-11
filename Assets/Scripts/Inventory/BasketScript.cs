using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketScript : ItemScript
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform heldBasketParent;
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
        SetLayerRecursively(gameObject, 8);
        animator.SetBool("Open", true);
        gameObject.transform.position = heldBasketParent.position;
        gameObject.transform.rotation = heldBasketParent.rotation;
        gameObject.transform.Rotate(0f, -90f, 0f);
        gameObject.transform.SetParent(heldBasketParent);
    }

    public override void OnDrop()
    {
        Inventory.Instance.DropBasket();
        gameObject.GetComponent<BoxCollider>().enabled = true;
        animator.SetBool("False", true);
        SetLayerRecursively(gameObject, 7);
    }

    private void  SetLayerRecursively(GameObject gameObject , int newLayer)
    {
        gameObject.layer = newLayer;

        foreach (Transform child in gameObject.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
