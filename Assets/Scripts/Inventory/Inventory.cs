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


    public int TotalPrice()
    {
        int total = 0;

        if (Items != null)
        {
            foreach (ItemDataSO item in Items)
            {
                total += item.price;
            }
        }

        return total;
    }
    /// <summary>
    /// Takes the TotalPrice Integer and converts it into a correctly formatted string
    /// ie: a total price of 99 becomes £0.99
    /// </summary>
    /// <returns>a formatted string of the total price</returns>
    public string PriceTotal()
    {
        string total = TotalPrice().ToString();

        int dotIndex = 2;
        dotIndex = Mathf.Min(total.Length, dotIndex);

        if (total.Length < 3)
        {

            for (int i = dotIndex; i < 3; i++)
            {
                total = "0" + total;
            }

        }
       
        total = "£" + total.Substring(0, total.Length - 2) + "." + total.Substring(total.Length - 2);
                   

        return total;
    }

}
