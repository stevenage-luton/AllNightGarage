using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public int price;
    public GameObject itemPrefab;
}
