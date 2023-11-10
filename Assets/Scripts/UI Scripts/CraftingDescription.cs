using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class CraftingDescription : MonoBehaviour
{
    [Header("Recipe")]
    [SerializeField] GameObject craftingPanel;
    [SerializeField] GameObject DescriptionPanel;
    [SerializeField] Image craftingImage;
    [SerializeField] TextMeshProUGUI titleTxt;
    [SerializeField] TextMeshProUGUI categoryTxt;
    [SerializeField] TextMeshProUGUI descriptionTxt;

    [Header("crafting Material")]
    public CraftingMaterial materialPrefabs;
    public RectTransform contentMaterial;
    List<CraftingMaterial> listOfMaterials = new List<CraftingMaterial>();

    [SerializeField] int maxString = 150;
    //[SerializeField] UiInventoryItem[] craftingMaterial;

    private void Awake()
    {
        resetCraftingRecipe();
    }

    private void Start()
    {
        listOfMaterials = new List<CraftingMaterial>();
    }

    public void resetCraftingRecipe()
    {
        craftingPanel.SetActive(false);
    }

    public void setCraftingRecipe(Sprite sprite, string craftingName, string category, string description, ItemSO[] craftingMaterial)
    {
        
        craftingImage.sprite = sprite;
        titleTxt.text = category;
        categoryTxt.text = category;
        if(description == "")
        {
            DescriptionPanel.SetActive(false);
        }
        else if(description.Length >= 150)
        {
            DescriptionPanel.SetActive(true);
            descriptionTxt.text = description.Substring(0, maxString);
        }
        else
        {
            DescriptionPanel.SetActive(true);
            descriptionTxt.text = description;
        }
        foreach(var i in craftingMaterial)
        {
            if(craftingMaterial == null)
            {
                return;
            }
            CraftingMaterial material = Instantiate(materialPrefabs, Vector3.zero, quaternion.identity);
            material.transform.SetParent(contentMaterial, false);
            listOfMaterials.Add(material);
            material.setMaterial(i.itemImage, i.itemName, i.itemQuantity, i.craftMaterialNeeded);
        }
    }

    public void show()
    {
        craftingPanel.SetActive(true);
    }

    public void hide()
    {
        clearCraftRecipe();
        craftingPanel.SetActive(false);
    }

    public void clearCraftRecipe()
    {
        foreach (var item in listOfMaterials)
        {
            Destroy(item.gameObject);
        }
        listOfMaterials.Clear();
    }
}
