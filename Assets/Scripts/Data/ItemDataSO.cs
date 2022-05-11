using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public int price;

    public float rotateAmount;
    public GameObject itemPrefab;
    [Range(0, 1f)]
    public float zoomDistance;

}
