using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingItem : MonoBehaviour
{
    [SerializeField] public Image craftingImage;
    [SerializeField] public Image canCraft;
    [SerializeField] public Image itemBorderImage;
    [SerializeField] public GameObject itemFootNote;
    public TextMeshProUGUI footTitle;
    public TextMeshProUGUI footQuantity;
    [SerializeField] bool isEmpty = true;

    [SerializeField] public CraftingSO craftPass;


    [SerializeField] public CraftingDescription craftingGuide;

    public event Action<CraftingItem> OnCraftClicked,
        OnRightMouseButtonClicked;

    private void Awake()
    {
        craftingGuide = FindObjectOfType<CraftingDescription>();
        resetData();
        Deselect();
    }

    public void resetData()
    {
        craftingImage.gameObject.SetActive(false);
        isEmpty = true;
    }

    public void Deselect()
    {
        itemBorderImage.enabled = false;
    }

    public void SetData(Sprite sprite, string fTitle, int fQuant, CraftingSO crafting)
    {
        if(fQuant == 0)
        {
            canCraft.enabled = false;
        }
        //footnote
        footTitle.text = fTitle;
        footQuantity.text = fQuant.ToString();
        //item
        craftingImage.gameObject.SetActive(true);
        craftingImage.sprite = sprite;
        isEmpty = false;
        craftPass = crafting;
        craftPass.craftingImage = sprite;
        
    }

    public void Select()
    {
        itemBorderImage.enabled = true;
    }

    public void OnPointerClick(BaseEventData obj)
    {
        PointerEventData pointer = (PointerEventData)obj;
        if (pointer.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClicked?.Invoke(this);
        }
        else
        {
            OnCraftClicked?.Invoke(this);
        }
    }


    public void OnPointerEnter(BaseEventData obj)
    {
        if (this.isEmpty == true)
        {
            return;
        }
        //craftingGuide.show();
        //craftingGuide.setCraftingRecipe(craftPass.craftingImage,craftPass.craftingName,craftPass.craftingCategory,craftPass.craftingDescription, craftPass.craftingMaterial);
        itemBorderImage.enabled = true;
        itemFootNote.SetActive(true);
    }

    public void OnPointerExit(BaseEventData obj)
    {
        if (this.isEmpty == true)
        {
            return;
        }
        //craftingGuide.hide();
        itemBorderImage.enabled = false;
        itemFootNote.SetActive(false);
    }
}
