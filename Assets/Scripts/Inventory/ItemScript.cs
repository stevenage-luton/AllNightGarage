using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] protected ItemDataSO m_ItemDataSO;

    public string PriceString()
    {
        string total = m_ItemDataSO.price.ToString();

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

    public virtual void OnPickup()
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

    public virtual void OnDrop()
    {
        ItemPickup itemPickup = GameObject.Find("PlayerCamera").GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            itemPickup.DropCurrentItem();
        }
    }

}
