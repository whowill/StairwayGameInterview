using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject helper;

    [SerializeField] Animator animationCraftingCanvas;
    [SerializeField] Animator animationDonationCanvas;
    [SerializeField] Animator animationCraftingDescription;

    [Header("Backpack inventory")]
    [SerializeField] InventorySO bag;
    [SerializeField] UiInventoryItem itemPrefabs;
    List<UiInventoryItem> listOfItems = new List<UiInventoryItem>();

    [SerializeField] Button craftingButton;
    [SerializeField] Button donationButton;
  
    [Header("Crafting UI")]
    [SerializeField] GameObject CraftingCanvas;
    [SerializeField] RectTransform bagCrafting;
    [SerializeField] GameObject craftingPanel;
    [SerializeField] GameObject anotherPanel;
    [SerializeField] TextMeshProUGUI anotherPanelTxt;
    
    [Header("crafting Recipe")]
    [SerializeField] CraftingInventorySO myRecipe;
    [SerializeField] CraftingItem craftPrefabs;
    [SerializeField] CraftingDescription craftingGuide;
    List<CraftingItem> listOfCrafts = new List<CraftingItem>();

    [Header("Crafting Backpack")]
    [SerializeField] RectTransform bagRecipeCrafting;

    [Header("Donation UI")]
    [SerializeField] GameObject DonationCanvas;
    [SerializeField] RectTransform bagDonation;
    [SerializeField] Slider sliderLevel;
    [SerializeField] ScrollRect rewardDonation;
    [SerializeField] int maxContributionXP;
    [SerializeField] int levelContribution = 0;
    [SerializeField] TextMeshProUGUI contributionTxt;

    [Header("Donation Reward")]
    [SerializeField] GameObject notifDonation;
    [SerializeField] Image imageDonation;
    [SerializeField] TextMeshProUGUI nameDonation;
    [SerializeField] TextMeshProUGUI categoryDonation;

    [Header("Donation Reward")]
    [SerializeField] GameObject notifReward;



    void Start() {
        listOfItems = new List<UiInventoryItem>();
        listOfCrafts = new List<CraftingItem>();
        craftingButton.onClick.AddListener(() => showCraftingCanvas());
        donationButton.onClick.AddListener(() => showDonationCanvas());    
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.I)){
            if(CraftingCanvas.activeInHierarchy == false && DonationCanvas.activeInHierarchy == true){
                hideDonationcanvas();
                helper.gameObject.SetActive(false);
                showCraftingCanvas();
            }else if(CraftingCanvas.activeInHierarchy == false){
                helper.gameObject.SetActive(false);
                showCraftingCanvas();
            }else{
                animationCraftingCanvas.SetBool("CloseCanvas", true);
                helper.gameObject.SetActive(true);
                craftingGuide.hide();
                hideCraftingCanvas();
            }
        }

        if(Input.GetKeyDown(KeyCode.J)){
            if(DonationCanvas.activeInHierarchy == false && CraftingCanvas.activeInHierarchy == true){
                hideCraftingCanvas();
                helper.gameObject.SetActive(false);
                showDonationCanvas();
                //StartCoroutine(scrollContentReward(0));
                contributionTxt.text = levelContribution.ToString();
            }else if (DonationCanvas.activeInHierarchy == false){

                showDonationCanvas();
                contributionTxt.text = levelContribution.ToString();
                //StartCoroutine(scrollContentReward(0));
                helper.gameObject.SetActive(false);
            }
            else{
                helper.gameObject.SetActive(true);
                hideDonationcanvas();
            }
        }
    }

    #region recipe Category
    public void categoryFunc(int num, RectTransform content)
    {
        clearCraftRecipe();
        foreach (var i in myRecipe.recipe)
        {
            if (i == null)
            {
                CraftingItem craft = Instantiate(craftPrefabs, Vector3.zero, quaternion.identity);
                craft.transform.SetParent(content, false);
                listOfCrafts.Add(craft);
                craft.OnCraftClicked += HandleCraftSelect;
                craft.OnRightMouseButtonClicked += HandleClickedCraft;
            }
            else
            {
                if(i.craftCategory == num) 
                { 
                    CraftingItem craft = Instantiate(craftPrefabs, Vector3.zero, quaternion.identity);
                    craft.transform.SetParent(content, false);
                    craft.SetData(i.craftImage, i.craftName, i.craftQuantity, i.craftingGuide);
                    listOfCrafts.Add(craft);
                    craft.OnCraftClicked += HandleCraftSelect;
                    craft.OnRightMouseButtonClicked += HandleClickedCraft;
                }
            }
        }
    }

    public void SetPanel(int num)
    {
        craftingPanel.SetActive(false);
        anotherPanel.SetActive(true);
    }

    public void buttonCategory(int num)
    {
        categoryFunc(num, bagRecipeCrafting);
    }

    public void buttonChangePanel(int num)
    {
        SetPanel(num);
    }

    public void setTextPanel(string text)
    {
        anotherPanelTxt.text = text;
    }

    #endregion

    #region initialize Function
    public void InventorySizeFunc(RectTransform content){
        clearInventory();
        int indexAt = 0;
        foreach(var i in bag.item){
            if(i == null)
            {
                UiInventoryItem item = Instantiate(itemPrefabs, Vector3.zero, quaternion.identity);
                item.transform.SetParent(content, false);
                listOfItems.Add(item);
                item.OnItemClicked += HandleItemSelect;
                item.OnRightMouseButtonClicked += HandleClickedItem;
            }
            else
            {
                UiInventoryItem item = Instantiate(itemPrefabs, Vector3.zero, quaternion.identity);
                item.transform.SetParent(content, false);
                if(i.isDonateable == false && !CraftingCanvas.activeInHierarchy)
                {
                    item.GetComponent<CanvasGroup>().enabled = true;
                }
                item.SetData(i.itemImage, i.itemQuantity, i.itemName, i.category, i, indexAt);
                listOfItems.Add(item);
                item.OnItemClicked += HandleItemSelect;
                item.OnRightMouseButtonClicked += HandleClickedItem;
                indexAt++;
            }
                
        }
    }

    public void craftingSizeFunc(RectTransform content)
    {
        clearCraftRecipe();
        foreach(var i in myRecipe.recipe)
        {
            if(i == null)
            {
                CraftingItem craft = Instantiate(craftPrefabs, Vector3.zero, quaternion.identity);
                craft.transform.SetParent(content, false);
                listOfCrafts.Add(craft);
                craft.OnCraftClicked += HandleCraftSelect;
                craft.OnRightMouseButtonClicked += HandleClickedCraft;
            }
            else
            {
                CraftingItem craft = Instantiate(craftPrefabs, Vector3.zero, quaternion.identity);
                craft.transform.SetParent(content, false);
                craft.SetData(i.craftImage, i.craftName, i.craftQuantity, i.craftingGuide);
                listOfCrafts.Add(craft);
                craft.OnCraftClicked += HandleCraftSelect;
                craft.OnRightMouseButtonClicked += HandleClickedCraft;
            }
        }
    }
    #endregion

    #region click Event
    private void HandleClickedCraft(CraftingItem obj)
    {
        throw new NotImplementedException();
    }

    private void HandleCraftSelect(CraftingItem obj)
    {
        obj.craftingGuide.hide();
        obj.craftingGuide.show();
        //animationCraftingCanvas.Play("CraftingRecipe");
        obj.craftingGuide.setCraftingRecipe(obj.craftPass.craftingImage, obj.craftPass.craftingName, obj.craftPass.craftingCategory, 
            obj.craftPass.craftingDescription, obj.craftPass.craftingMaterial);
    }

    private void HandleClickedItem(UiInventoryItem obj)
    {
        throw new NotImplementedException();
    }

    private void HandleItemSelect(UiInventoryItem obj)
    {
        if(this.CraftingCanvas.activeInHierarchy )
        {
            Debug.Log("you click item on inventory");
            return;
        }
        else if(obj.itemPass.isDonateable == true)
        {
            if(obj.itemPass.itemQuantity - 1 == 0)
            {
                Debug.Log("you click item on donation");
                setNotifDonated(obj.itemPass.itemImage, obj.itemPass.itemName, obj.itemPass.category);
                sliderLevel.value += obj.itemPass.donationValue;
                if (sliderLevel.value >= 100)
                {
                    levelContribution += 1;
                    StartCoroutine(scrollContentReward(0));
                    showReward();
                    sliderLevel.value = 0;
                    contributionTxt.text = levelContribution.ToString();
                }
                Destroy(obj.gameObject);
                bag.item.RemoveAt(obj.indexPass);
                listOfItems.RemoveAt(obj.indexPass);
                
            }
            else
            {
                sliderLevel.value += obj.itemPass.donationValue;
                if(sliderLevel.value >= 100)
                {
                    levelContribution += 1;
                    StartCoroutine(scrollContentReward(0));
                    showReward();
                    sliderLevel.value = 0;
                    contributionTxt.text = levelContribution.ToString();
                }
                Debug.Log("you click item on donation");
                setNotifDonated(obj.itemPass.itemImage, obj.itemPass.itemName, obj.itemPass.category);
                obj.itemPass.itemQuantity -= 1;
                obj.quantityTxt.text = obj.itemPass.itemQuantity.ToString();
            }
        }
        else
        {
            return;
        }
    }

#endregion

    #region Function clear and setActive gameObject
    public void setNotifDonated(Sprite image, string name, string category)
    {
        notifDonation.gameObject.SetActive(true);
        imageDonation.sprite = image;
        nameDonation.text = name;
        categoryDonation.text = category;
        StartCoroutine(timerDonated(2f));
    }
    public void showReward()
    {
        notifReward.gameObject.SetActive(true);
    }

    public void claimReward()
    {
        notifReward.gameObject.SetActive(false);
    }

    IEnumerator timerDonated(float time)
    {
        yield return new WaitForSeconds(time);
        notifDonation.gameObject.SetActive(false);
    }

    IEnumerator canvasCraftingClose(float time)
    {
        animationCraftingCanvas.SetBool("CloseCanvas", true);
        yield return new WaitForSeconds(time);
        CraftingCanvas.gameObject.SetActive(false);
    }

    IEnumerator canvasDonationClose(float time)
    {
        animationDonationCanvas.SetBool("CloseDonation", true);
        yield return new WaitForSeconds(time);
        DonationCanvas.gameObject.SetActive(false);
    }

    IEnumerator scrollContentReward(float time)
    {
        yield return new WaitForSeconds(time);
        rewardDonation.horizontalNormalizedPosition += 0.06f;
    }

    public void showCraftingCanvas(){
        helper.gameObject.SetActive(false);
        anotherPanel.SetActive(false);
        craftingPanel.SetActive(true);
        CraftingCanvas.gameObject.SetActive(true);
        InventorySizeFunc(bagCrafting);
        craftingSizeFunc(bagRecipeCrafting);
    }


    public void showDonationCanvas(){
        helper.gameObject.SetActive(false);
        DonationCanvas.gameObject.SetActive(true);
        InventorySizeFunc(bagDonation);
    }

    public void hideCraftingCanvas(){
        helper.gameObject.SetActive(true);
        clearInventory();
        clearCraftRecipe();
        StartCoroutine(canvasCraftingClose(0.5f));
        //CraftingCanvas.gameObject.SetActive(false);
    }

    public void clearInventory()
    {
        foreach (var item in listOfItems)
        {
            Destroy(item.gameObject);
        }
        listOfItems.Clear();
    }

    public void clearCraftRecipe()
    {
        foreach (var item in listOfCrafts)
        {
            Destroy(item.gameObject);
        }
        listOfCrafts.Clear();
    }

    public void hideDonationcanvas(){
        
        helper.gameObject.SetActive(true);
        foreach(var item in listOfItems){
            Destroy(item.gameObject);
        }
        listOfItems.Clear();
        StartCoroutine(canvasDonationClose(0.5f));
        //DonationCanvas.gameObject.SetActive(false);
    }

    #endregion


}
