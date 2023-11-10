using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "ScriptableObject/RecipeSO", order = 1)]
public class RecipeSO : ScriptableObject
{
    [Header("item")]
    [SerializeField] public Sprite craftImage;

    [Header("footNote")]
    [SerializeField] public int craftCategory;
    [SerializeField] public string craftName;
    [SerializeField] public int craftQuantity;

    public CraftingSO craftingGuide;
}
