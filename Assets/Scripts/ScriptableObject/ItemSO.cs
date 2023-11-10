using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "itemSO", menuName = "ScriptableObject/itemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    [Header("footNote")]
    [SerializeField] public string itemName;
    [SerializeField] public string category;
    [Header("item")]
    [SerializeField] public int itemQuantity;
    [SerializeField] public Sprite itemImage;

    [SerializeField] public bool isDonateable;
    public int donationValue;
    [SerializeField] public int craftMaterialNeeded;
}
