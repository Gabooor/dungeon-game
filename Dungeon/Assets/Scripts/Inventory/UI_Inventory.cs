using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private Button[] slots;

    private Transform itemSlotBackground;
    private Transform itemSlotBackgroundTemplate;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private float itemSlotCellSize = 100f;
    public static int gridSize = 70;

    public void Awake(){
        itemSlotBackground = transform.Find("itemSlotBackground");
        itemSlotBackgroundTemplate = itemSlotBackground.Find("itemSlotBackgroundTemplate");

        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        slots = new Button[gridSize];
        CreateGrid();
    }

    public void SetInventory(Inventory inventory){
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        //RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e){
        RefreshInventoryItems();
    }

    public void CreateGrid(){
        int x = 0;
        int y = 0;
        for(int i = 0; i < gridSize; i++){
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + 20, (y * itemSlotCellSize) - 20);
            x++;
            if(x > 6){
                x = 0;
                y--;
            }
        }
        x = 0;
        y = 0;
    }

    public void RefreshInventoryItems(){

    }

    /*public void RefreshInventoryItems(){
        int x = 0;
        int y = 0;
        foreach(Transform child in itemSlotContainer){
            if(child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(Item item in inventory.GetItemList()){
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + 20, (y * itemSlotCellSize) - 20);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if(x > 6){
                x = 0;
                y--;
            }
        }
    }*/

}
