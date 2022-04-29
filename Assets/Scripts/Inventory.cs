using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Too many Inventories!!");
            return;
        }

        Instance = this;    
    }
    #endregion 

    public List<ItemDataSO> Items = new List<ItemDataSO>();

    public bool hasBasket = false;

    public void AddItem(ItemDataSO itemToAdd)
    {
        Items.Add(itemToAdd);
    }

    public void RemoveItem(ItemDataSO itemToRemove)
    {
        Items.Remove(itemToRemove);
    }

    public void TakeBasket()
    {
        hasBasket = true;
    }

    public void DropBasket()
    {
        hasBasket = false;
    }
}
