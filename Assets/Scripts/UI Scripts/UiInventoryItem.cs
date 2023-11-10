using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UiInventoryItem : MonoBehaviour
{
    [SerializeField] public Image itemImage;
    [SerializeField] public TextMeshProUGUI quantityTxt;
    [SerializeField] public Image itemBorderImage;
    [SerializeField] public GameObject itemFootNote;
    public TextMeshProUGUI footName;
    public TextMeshProUGUI footCategory;
    [SerializeField] public bool isEmpty = true;

    [SerializeField] public ItemSO itemPass;
    public int indexPass;

    public event Action<UiInventoryItem> OnItemClicked,
        OnRightMouseButtonClicked;

    private void Awake()
    {
        resetData();
        Deselect();
        
    }

    public void resetData()
    {
        itemImage.gameObject.SetActive(false);
        isEmpty = true;
    }

    public void Deselect()
    {
        itemBorderImage.enabled = false;
    }
    
    public void SetData(Sprite sprite, int Quantity, string fname, string fcategory, ItemSO item, int indexAt)
    {
        indexPass = indexAt;
        //footNote;
        footName.text = fname;
        footCategory.text = fcategory;
        //item
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        quantityTxt.text = Quantity.ToString();
        itemPass = item;
        isEmpty = false;
    }

    public void OnPointerClick(BaseEventData obj)
    {
        PointerEventData pointer = (PointerEventData)obj;
        if(pointer.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnPointerEnter(BaseEventData obj)
    {
        if(this.isEmpty == true)
        {
            return;
        }
        itemBorderImage.enabled = true;
        itemFootNote.SetActive(true);
    }

    public void OnPointerExit(BaseEventData obj)
    {
        if (this.isEmpty == true)
        {
            return;
        }
        itemBorderImage.enabled = false;
        itemFootNote.SetActive(false);
    }
}
