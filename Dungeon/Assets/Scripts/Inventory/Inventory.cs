using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory{
    public event EventHandler OnItemListChanged; 

    public int maxItems;

    //private List<Item> itemList;
    private Item[] itemList;

    public Inventory(){
        //itemList = new List<Item>();
        maxItems = UI_Inventory.gridSize;
        itemList = new Item[maxItems];
    }

    public void AddItem(Item item){
        /*if(itemList.Count == maxItems){
            Debug.Log("Inventory is full.");
        }
        else{
            itemList.Add(item);
            OnItemListChanged?.Invoke(this,EventArgs.Empty);
        }*/
        for(int i = 0; i < maxItems; i++){
            if(itemList[i] == null){
                itemList[i] = item;
                break;
            }
            Debug.Log("test");
        }
    }

    public Item[] GetItemList(){
        return itemList;
    }

    /*public List<Item> GetItemList(){
        return itemList;
    }*/
}
