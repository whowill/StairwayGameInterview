using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMaterial : MonoBehaviour
{
    public Image imageMaterial;
    public TextMeshProUGUI nameMaterialTxt;
    public TextMeshProUGUI quantityMaterialTxt;

    public void setMaterial(Sprite image, string name, int itemOwn, int itemNeeded)
    {
        imageMaterial.sprite = image;
        nameMaterialTxt.text = name;
        quantityMaterialTxt.text = itemOwn + " / " + itemNeeded;
    }
}
