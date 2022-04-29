using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] private ItemDataSO m_ItemDataSO;

    public void OnPickup()
    {
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.PickupItem(m_ItemDataSO);
            itemPickup.ExamineHeldItem(gameObject);
        }
    }

    public void AddToInventory()
    {
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.AddItemToInventory(m_ItemDataSO);
            Destroy(gameObject);
        }
    }

    public void OnDrop()
    {
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.DropCurrentItem();
        }
    }
}
