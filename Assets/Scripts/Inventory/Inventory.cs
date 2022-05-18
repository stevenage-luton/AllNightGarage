using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Inventory class. Implemented as a Singleton so we don't need a bunch of references.
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// the singleton instance. If there's more than one, print a debug message.
    /// </summary>
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
    /// <summary>
    /// The list of items in the Inventory
    /// </summary>
    public List<ItemDataSO> Items = new List<ItemDataSO>();

    public bool paidForItems = false;
    /// <summary>
    /// Does the player have a basket?
    /// </summary>
    public bool hasBasket = false;
    /// <summary>
    /// Method for adding item to inventory list.
    /// </summary>
    /// <param name="itemToAdd">The item to add</param>
    public void AddItem(ItemDataSO itemToAdd)
    {
        Items.Add(itemToAdd);
    }
    /// <summary>
    /// Method for removing item from inventory list.
    /// </summary>
    /// <param name="itemToRemove">the item to remove.</param>
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

    /// <summary>
    /// Adds up the value of every item in the Inventory list as an Integer.
    /// </summary>
    /// <returns>total of all price values as an integer</returns>
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
