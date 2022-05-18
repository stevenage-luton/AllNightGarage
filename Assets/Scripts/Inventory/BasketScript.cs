using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Extends ItemScript with unique methods for pickup and drop
/// </summary>
public class BasketScript : ItemScript
{
    /// <summary>
    /// so we can tell the animator what to do.
    /// </summary>
    [SerializeField]
    private Animator animator;
    /// <summary>
    /// the parent object for when we pick up the basket.
    /// </summary>
    [SerializeField]
    private Transform heldBasketParent;
    /// <summary>
    /// sets the hasBasket bool in the Inventory Singleton Instance, tells the UI that we have a basket, and then does the rest of the base pickup method.
    /// </summary>
    public override void OnPickup()
    {
        Inventory.Instance.TakeBasket();
        UIScript.TakeBasket();
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.PickupItem(_ItemDataSO);
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        SetLayerRecursively(gameObject, 8);
        animator.SetBool("Open", true);
        gameObject.transform.position = heldBasketParent.position;
        gameObject.transform.rotation = heldBasketParent.rotation;
        gameObject.transform.Rotate(0f, -90f, 0f);
        gameObject.transform.SetParent(heldBasketParent);
    }
    /// <summary>
    /// calls the drop method in our inventory Singleton, disables the collider so the player doesn't constantly clip backwards into infinity,
    /// and sets layer and layer of all children to the default Item layer
    /// </summary>
    public override void OnDrop()
    {
        Inventory.Instance.DropBasket();
        gameObject.GetComponent<BoxCollider>().enabled = true;
        animator.SetBool("False", true);
        SetLayerRecursively(gameObject, 7);
    }

}
