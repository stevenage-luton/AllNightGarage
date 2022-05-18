using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for Items. Contains Itemdata for the item and generic methods for picking up and examining the object.
/// </summary>
public class ItemScript : Interactable
{
    [SerializeField] protected ItemDataSO _ItemDataSO;

    public string PriceString()
    {
        string total = _ItemDataSO.price.ToString();

        int dotIndex = 2;
        dotIndex = Mathf.Min(total.Length, dotIndex);

        if (total != null){
            if (total.Length < 3)
            {
                for (int i = dotIndex; i < 3; i++)
                {
                    total = "0" + total;
                }
            }
        }


        total = "£" + total.Substring(0, total.Length - 2) + "." + total.Substring(total.Length - 2);

        return total;
    }

    public override void OnClick()
    {
        gameObject.GetComponent<ItemScript>().OnPickup();
    }

    public virtual void OnPickup()
    {
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.PickupItem(_ItemDataSO);
            itemPickup.ExamineHeldItem(gameObject);
        }
    }

    public void AddToInventory()
    {
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.AddItemToInventory(_ItemDataSO);
            Destroy(gameObject);
        }
    }

    public virtual void OnDrop()
    {
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.DropCurrentItem();
        }
    }

    /// <summary>
    /// Recursively sets object and all children to a given layer
    /// </summary>
    /// <param name="gameObject">the object to set</param>
    /// <param name="newLayer">the layer to set everything to</param>
    protected void SetLayerRecursively(GameObject gameObject, int newLayer)
    {
        gameObject.layer = newLayer;

        foreach (Transform child in gameObject.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}
