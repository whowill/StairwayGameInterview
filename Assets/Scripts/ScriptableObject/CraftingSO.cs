using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "craftingSO", menuName = "ScriptableObject/craftingSO", order = 1)]
public class CraftingSO : ScriptableObject
{
    public Sprite craftingImage;
    [SerializeField] public string craftingName;
    [SerializeField] public string craftingCategory;

    [SerializeField] [field: TextArea] public string craftingDescription;


    [SerializeField] public ItemSO[] craftingMaterial;
}
