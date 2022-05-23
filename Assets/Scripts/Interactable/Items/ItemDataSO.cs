using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public int price;

    public float rotateAmount;
    public GameObject itemPrefab;
    [Range(-1f, 1f)]
    public float zoomDistance;

    public AudioClip pickupSound;
    public AudioClip dropSound;
}
