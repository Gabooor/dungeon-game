using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIdentifier : MonoBehaviour
{
    public int buttonIndex;

    public void SetIndex(int index){
        this.buttonIndex = index;
    }

    public void GetIndex(){
        for (int i = 0; i < Player.inventorySpace+7; i++) {
            if(i == this.buttonIndex){
                if(InventoryUI.isButton1Clicked == false){
                    if(i < Player.inventorySpace){
                        InventoryUI.isButton1Clicked = true;
                        InventoryUI.button1Index = i;
                        foreach(Transform child in InventoryUI.buttons[i]){
                            if(Player.inventory[i] == null){
                                if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                            }
                            else{
                                if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else{
                        InventoryUI.isButton1Clicked = true;
                        InventoryUI.button1Index = i;
                        if(i == Player.inventorySpace){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.helmet == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+1){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.chestplate == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+2){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.leggings == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+3){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.boots == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+4){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.amulet == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+5){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.ring == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+6){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.weapon == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                    }
                    //Debug.Log(i);
                }
                else{
                    if(i < Player.inventorySpace){
                                InventoryUI.isButton2Clicked = true;
                                InventoryUI.button2Index = i;
                        foreach(Transform child in InventoryUI.buttons[i]){
                            if(Player.inventory[i] == null){
                                if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                            }
                            else{
                                if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else{
                        InventoryUI.isButton2Clicked = true;
                        InventoryUI.button2Index = i;
                        if(i == Player.inventorySpace){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.helmet == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+1){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.chestplate == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+2){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.leggings == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+3){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.boots == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+4){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.amulet == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+5){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.ring == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                        else if(i == Player.inventorySpace+6){
                            foreach(Transform child in InventoryUI.buttons[i]){
                                if(Player.weapon == null){
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(106,87,87,0);
                                }
                                else{
                                    if(child.gameObject.name == "background") child.GetComponent<Image>().color = new Color32(84,176,49,255);
                                    if(child.gameObject.name == "image") child.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                            }
                        }
                    }
                    
                    //Debug.Log(i);
                }
                break;
            }
        }
    }

}
