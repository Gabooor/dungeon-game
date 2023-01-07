using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory{
    //public event EventHandler OnItemListChanged; 

    public int maxItems = 70;

    private Item[] itemList;

    public Inventory(){
        itemList = new Item[maxItems];
    }

    public void AddItem(Item item){
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
}
