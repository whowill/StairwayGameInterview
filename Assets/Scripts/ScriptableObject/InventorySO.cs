using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventorySO", menuName = "ScriptableObject/inventorySO", order = 1)]
public class InventorySO : ScriptableObject
{
    [SerializeField] public List<ItemSO> item;
}
