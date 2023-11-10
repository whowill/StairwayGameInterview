using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingInventSO", menuName = "ScriptableObject/CraftingInventSO", order = 1)]
public class CraftingInventorySO : ScriptableObject
{
    [SerializeField] public List<RecipeSO> recipe;
}
