using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static Transform[] buttons;

    public Transform inventoryGrid;
    public Transform slotTemplate;
    
    [Header("Main Panels")]
    public bool isInventoryOpen;
    public GameObject InventoryPanel;
    public GameObject EquipmentPanel;

    // public Transform HelmetPanel;
    // public Transform ChestplatePanel;
    // public Transform LeggingsPanel;
    // public Transform BootsPanel;
    
    // public static GameObject WeaponPanel;

    private float itemSlotCellSize = 100f;

    public static bool isButton1Clicked;
    public static bool isButton2Clicked;
    public static int button1Index;
    public static int button2Index;

    public Transform player;

    void Start(){
        buttons = new Transform[Player.inventorySpace+7];
        CreateGrid();
        // for(int i = 0; i < buttons.Length; i++){
        //     Debug.Log(buttons[i].gameObject.name);
        // }
    
        isButton1Clicked = false;
        isButton2Clicked = false;

        // HelmetPanel = Transform.Find("HelmetSlot");
        // ChestplatePanel = Transform.Find("ChestplateSlot");
        // LeggingsPanel = Transform.Find("LeggingsSlot");
        // BootsPanel = Transform.Find("BootsSlot");
        
        // if(Player.helmet != null){
        //     foreach(Transform child in HelmetPanel){
        //         if(child.gameObject.name == "image"){
        //             child.sprite = Player.helmet.GetSprite();
        //         }
        //     }
        // }
        // if(Player.chestplate != null){
        //     foreach(Transform child in ChestplatePanel){
        //         if(child.gameObject.name == "image"){
        //             child.sprite = Player.chestplate.GetSprite();
        //         }
        //     }
        // }
        // if(Player.leggings != null){
        //     foreach(Transform child in LeggingsPanel){
        //         if(child.gameObject.name == "image"){
        //             child.sprite = Player.leggings.GetSprite();
        //         }
        //     }
        // }
        // if(Player.boots != null){
        //     foreach(Transform child in BootsPanel){
        //         if(child.gameObject.name == "image"){
        //             child.sprite = Player.boots.GetSprite();
        //         }
        //     }
        // }

        // WeaponPanel = GameObject.Find("WeaponSlot");
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(!isInventoryOpen){
                InventoryPanel.SetActive(true);
                EquipmentPanel.SetActive(true);
                isInventoryOpen = true;
            }
            else{
                InventoryPanel.SetActive(false);
                EquipmentPanel.SetActive(false);
                isInventoryOpen = false;
            }
        }
        if(isButton2Clicked){
            SwapItems();
        }
    }

    void CreateGrid(){
        int x = 0;
        int y = 0;
        for(int i = 0; i < Player.inventorySpace; i++){
            RectTransform slotRectTransform = Instantiate(slotTemplate, inventoryGrid).GetComponent<RectTransform>();
            slotRectTransform.gameObject.SetActive(true);
            slotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + 60, (y * itemSlotCellSize) - 60);
            x++;
            if(x > 6){
                x = 0;
                y--;
            }
        }
        x = 0;
        y = 0;
        int index = 0;
        foreach (Transform child in inventoryGrid)
        {
            buttons[index] = child;
            child.gameObject.name = "Slot" + index.ToString();
            foreach(Transform child2 in buttons[index]){
                if(child2.gameObject.name == "image"){
                    child2.GetComponent<ButtonIdentifier>().SetIndex(index);
                }
            }
            index++;
        }
        // Helmet Slot
        RectTransform slotRT = Instantiate(slotTemplate, EquipmentPanel.transform).GetComponent<RectTransform>();
        slotRT.gameObject.SetActive(true);
        slotRT.anchoredPosition = new Vector2(570,-210);
        // buttons[Player.inventorySpace] = slotRT.transform;
        // buttons[Player.inventorySpace].gameObject.name = "HelmetPanel";
        // foreach(Transform child in buttons[Player.inventorySpace]){
        //     if(child.gameObject.name == "image"){
        //         child.GetComponent<ButtonIdentifier>().SetIndex(Player.inventorySpace);
        //         child.GetComponent<Image>().sprite = Player.helmet.GetSprite();
        //     }
        // }

        // Chestplate Slot
        slotRT = Instantiate(slotTemplate, EquipmentPanel.transform).GetComponent<RectTransform>();
        slotRT.gameObject.SetActive(true);
        slotRT.anchoredPosition = new Vector2(570,-310);
        // buttons[Player.inventorySpace+1] = slotRT.transform;
        // buttons[Player.inventorySpace+1].gameObject.name = "ChestplatePanel";
        // foreach(Transform child in buttons[Player.inventorySpace]){
        //     if(child.gameObject.name == "image"){
        //         child.GetComponent<ButtonIdentifier>().SetIndex(Player.inventorySpace);
        //         child.GetComponent<Image>().sprite = Player.helmet.GetSprite();
        //     }
        // }
        
        // Leggings Slot
        slotRT = Instantiate(slotTemplate, EquipmentPanel.transform).GetComponent<RectTransform>();
        slotRT.gameObject.SetActive(true);
        slotRT.anchoredPosition = new Vector2(570,-410);
        
        // Boots Slot
        slotRT = Instantiate(slotTemplate, EquipmentPanel.transform).GetComponent<RectTransform>();
        slotRT.gameObject.SetActive(true);
        slotRT.anchoredPosition = new Vector2(570,-510);
        
        // Amulet Slot
        slotRT = Instantiate(slotTemplate, EquipmentPanel.transform).GetComponent<RectTransform>();
        slotRT.gameObject.SetActive(true);
        slotRT.anchoredPosition = new Vector2(150,-260);
        
        // Ring Slot
        slotRT = Instantiate(slotTemplate, EquipmentPanel.transform).GetComponent<RectTransform>();
        slotRT.gameObject.SetActive(true);
        slotRT.anchoredPosition = new Vector2(150,-360);
        
        // Weapon Slot
        slotRT = Instantiate(slotTemplate, EquipmentPanel.transform).GetComponent<RectTransform>();
        slotRT.gameObject.SetActive(true);
        slotRT.anchoredPosition = new Vector2(150,-460);

        int k = 0;
        foreach(Transform child in EquipmentPanel.transform){
            if(child.name == "SlotTemplate(Clone)"){
                buttons[Player.inventorySpace+k] = child;
                switch(k){
                    default:
                    case 0: child.gameObject.name = "HelmetPanel"; break;
                    case 1: child.gameObject.name = "ChestplatePanel"; break;
                    case 2: child.gameObject.name = "LeggingsPanel"; break;
                    case 3: child.gameObject.name = "BootsPanel"; break;
                    case 4: child.gameObject.name = "AmuletPanel"; break;
                    case 5: child.gameObject.name = "RingPanel"; break;
                    case 6: child.gameObject.name = "WeaponPanel"; break;
                }
                foreach(Transform child2 in buttons[Player.inventorySpace+k]){
                if(child2.gameObject.name == "image"){
                    child2.GetComponent<ButtonIdentifier>().SetIndex(Player.inventorySpace+k);
                    switch(k){
                        default: break;
                        case 0: if(Player.helmet != null){
                                    child2.GetComponent<Image>().sprite = Player.helmet.GetSprite();
                                    child2.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                                break;
                        case 1: if(Player.chestplate != null){
                                    child2.GetComponent<Image>().sprite = Player.chestplate.GetSprite();
                                    child2.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                                break;
                        case 2: if(Player.leggings != null){
                                    child2.GetComponent<Image>().sprite = Player.leggings.GetSprite();
                                    child2.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                                break;
                        case 3: if(Player.boots != null){
                                    child2.GetComponent<Image>().sprite = Player.boots.GetSprite();
                                    child2.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                                break;
                        case 4: if(Player.amulet != null){
                                    child2.GetComponent<Image>().sprite = Player.amulet.GetSprite();
                                    child2.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                                break;
                        case 5: if(Player.ring != null){
                                    child2.GetComponent<Image>().sprite = Player.ring.GetSprite();
                                    child2.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                                break;
                        case 6: if(Player.weapon != null){
                                    child2.GetComponent<Image>().sprite = Player.weapon.GetSprite();
                                    child2.GetComponent<Image>().color = new Color32(255,255,255,255);
                                }
                                break;
                    }
                }
            }
            k++;
            }
        }
    }

    public static void RefreshInventoryUI(){
        for(int i = 0; i < Player.inventorySpace+7; i++){
            if(i < Player.inventorySpace){
                foreach(Transform child in InventoryUI.buttons[i]){
                    if(Player.inventory[i] == null){
                        if(child.gameObject.name == "background"){
                            child.GetComponent<Image>().color = new Color32(91,91,91,255);
                        }
                        if(child.gameObject.name == "image"){
                            child.GetComponent<Image>().sprite = null;
                            child.GetComponent<Image>().color = new Color32(91,91,91,91);
                        }
                    }
                    else{
                        if(child.gameObject.name == "background"){
                            child.GetComponent<Image>().color = new Color32(91,91,91,255);
                        }
                        if(child.gameObject.name == "image"){
                            child.GetComponent<Image>().sprite = Player.inventory[i].GetSprite();
                            child.GetComponent<Image>().color = new Color32(255,255,255,255);
                        }
                    }
                }
            }
            else{
                foreach(Transform child in InventoryUI.buttons[i]){
                    if(i == Player.inventorySpace){
                        if(Player.helmet == null){
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = null;
                                child.GetComponent<Image>().color = new Color32(91,91,91,91);
                            }
                        }
                        else{
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = Player.helmet.GetSprite();
                                child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else if(i == Player.inventorySpace+1){
                        if(Player.chestplate == null){
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = null;
                                child.GetComponent<Image>().color = new Color32(91,91,91,91);
                            }
                        }
                        else{
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = Player.chestplate.GetSprite();
                                child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else if(i == Player.inventorySpace+2){
                        if(Player.leggings == null){
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = null;
                                child.GetComponent<Image>().color = new Color32(91,91,91,91);
                            }
                        }
                        else{
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = Player.leggings.GetSprite();
                                child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else if(i == Player.inventorySpace+3){
                        if(Player.boots == null){
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = null;
                                child.GetComponent<Image>().color = new Color32(91,91,91,91);
                            }
                        }
                        else{
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = Player.boots.GetSprite();
                                child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else if(i == Player.inventorySpace+4){
                        if(Player.amulet == null){
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = null;
                                child.GetComponent<Image>().color = new Color32(91,91,91,91);
                            }
                        }
                        else{
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = Player.amulet.GetSprite();
                                child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else if(i == Player.inventorySpace+5){
                        if(Player.ring == null){
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = null;
                                child.GetComponent<Image>().color = new Color32(91,91,91,91);
                            }
                        }
                        else{
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = Player.ring.GetSprite();
                                child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                    else if(i == Player.inventorySpace+6){
                        if(Player.weapon == null){
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = null;
                                child.GetComponent<Image>().color = new Color32(91,91,91,91);
                            }
                        }
                        else{
                            if(child.gameObject.name == "background"){
                                child.GetComponent<Image>().color = new Color32(91,91,91,255);
                            }
                            if(child.gameObject.name == "image"){
                                child.GetComponent<Image>().sprite = Player.weapon.GetSprite();
                                child.GetComponent<Image>().color = new Color32(255,255,255,255);
                            }
                        }
                    }
                }
            }
        }
    }

    public void SwapItems(){
        isButton1Clicked = false;
        isButton2Clicked = false;
        Debug.Log("Button1: " + buttons[button1Index]);
        Debug.Log("Button2: " + buttons[button2Index]);
        if((button1Index == Player.inventorySpace) || (button2Index == Player.inventorySpace)){
            // Helmet
            if((button1Index == Player.inventorySpace)){
                if((button2Index < Player.inventorySpace) && ((Player.inventory[button2Index] == null) || (Player.inventory[button2Index].isHelmet()))){
                    Item temp = Player.helmet;
                    Player.helmet = Player.inventory[button2Index];
                    Player.inventory[button2Index] = temp;
                }
            }
            else if((button2Index == Player.inventorySpace)){
                if((button1Index < Player.inventorySpace) && ((Player.inventory[button1Index] == null) || (Player.inventory[button1Index].isHelmet()))){
                    Item temp = Player.helmet;
                    Player.helmet = Player.inventory[button1Index];
                    Player.inventory[button1Index] = temp;
                }
            }
            RefreshInventoryUI();
            return;
        }
        else if((button1Index == Player.inventorySpace+1) || (button2Index == Player.inventorySpace+1)){
            // Chestplate
            if((button1Index == Player.inventorySpace+1)){
                if((button2Index < Player.inventorySpace) && ((Player.inventory[button2Index] == null) || (Player.inventory[button2Index].isChestplate()))){
                    Item temp = Player.chestplate;
                    Player.chestplate = Player.inventory[button2Index];
                    Player.inventory[button2Index] = temp;
                }
            }
            else if((button2Index == Player.inventorySpace+1)){
                if((button1Index < Player.inventorySpace) && ((Player.inventory[button1Index] == null) || (Player.inventory[button1Index].isChestplate()))){
                    Item temp = Player.chestplate;
                    Player.chestplate = Player.inventory[button1Index];
                    Player.inventory[button1Index] = temp;
                }
            }
            RefreshInventoryUI();
            return;
        }
        else if((button1Index == Player.inventorySpace+2) || (button2Index == Player.inventorySpace+2)){
            // Leggings
            if((button1Index == Player.inventorySpace+2)){
                if((button2Index < Player.inventorySpace) && ((Player.inventory[button2Index] == null) || (Player.inventory[button2Index].isLeggings()))){
                    Item temp = Player.leggings;
                    Player.leggings = Player.inventory[button2Index];
                    Player.inventory[button2Index] = temp;
                }
            }
            else if((button2Index == Player.inventorySpace+2)){
                if((button1Index < Player.inventorySpace) && ((Player.inventory[button1Index] == null) || (Player.inventory[button1Index].isLeggings()))){
                    Item temp = Player.leggings;
                    Player.leggings = Player.inventory[button1Index];
                    Player.inventory[button1Index] = temp;
                }
            }
            RefreshInventoryUI();
            return;
        }
        else if((button1Index == Player.inventorySpace+3) || (button2Index == Player.inventorySpace+3)){
            // Boots
            if((button1Index == Player.inventorySpace+3)){
                if((button2Index < Player.inventorySpace) && ((Player.inventory[button2Index] == null) || (Player.inventory[button2Index].isBoots()))){
                    Item temp = Player.boots;
                    Player.boots = Player.inventory[button2Index];
                    Player.inventory[button2Index] = temp;
                }
            }
            else if((button2Index == Player.inventorySpace+3)){
                if((button1Index < Player.inventorySpace) && ((Player.inventory[button1Index] == null) || (Player.inventory[button1Index].isBoots()))){
                    Item temp = Player.boots;
                    Player.boots = Player.inventory[button1Index];
                    Player.inventory[button1Index] = temp;
                }
            }
            RefreshInventoryUI();
            return;
        }
        else if((button1Index == Player.inventorySpace+4) || (button2Index == Player.inventorySpace+4)){
            // Amulet
            if((button1Index == Player.inventorySpace+4)){
                if((button2Index < Player.inventorySpace) && ((Player.inventory[button2Index] == null) || (Player.inventory[button2Index].isAmulet()))){
                    Item temp = Player.amulet;
                    Player.amulet = Player.inventory[button2Index];
                    Player.inventory[button2Index] = temp;
                }
            }
            else if((button2Index == Player.inventorySpace+4)){
                if((button1Index < Player.inventorySpace) && ((Player.inventory[button1Index] == null) || (Player.inventory[button1Index].isAmulet()))){
                    Item temp = Player.amulet;
                    Player.amulet = Player.inventory[button1Index];
                    Player.inventory[button1Index] = temp;
                }
            }
            RefreshInventoryUI();
            return;
        }
        else if((button1Index == Player.inventorySpace+5) || (button2Index == Player.inventorySpace+5)){
            // Ring
            if((button1Index == Player.inventorySpace+5)){
                if((button2Index < Player.inventorySpace) && ((Player.inventory[button2Index] == null) || (Player.inventory[button2Index].isRing()))){
                    Item temp = Player.ring;
                    Player.ring = Player.inventory[button2Index];
                    Player.inventory[button2Index] = temp;
                }
            }
            else if((button2Index == Player.inventorySpace+5)){
                if((button1Index < Player.inventorySpace) && ((Player.inventory[button1Index] == null) || (Player.inventory[button1Index].isRing()))){
                    Item temp = Player.ring;
                    Player.ring = Player.inventory[button1Index];
                    Player.inventory[button1Index] = temp;
                }
            }
            RefreshInventoryUI();
            return;
        }
        else if((button1Index == Player.inventorySpace+6) || (button2Index == Player.inventorySpace+6)){
            // Weapon
            if((button1Index == Player.inventorySpace+6)){
                if((button2Index < Player.inventorySpace) && ((Player.inventory[button2Index] == null) || (Player.inventory[button2Index].isWeapon()))){
                    Item temp = Player.weapon;
                    Player.weapon = Player.inventory[button2Index];
                    Player.inventory[button2Index] = temp;
                }
            }
            else if((button2Index == Player.inventorySpace+6)){
                if((button1Index < Player.inventorySpace) && ((Player.inventory[button1Index] == null) || (Player.inventory[button1Index].isWeapon()))){
                    Item temp = Player.weapon;
                    Player.weapon = Player.inventory[button1Index];
                    Player.inventory[button1Index] = temp;
                }
            }
            RefreshInventoryUI();
            return;
        }
        else{
            Item temp = Player.inventory[button1Index];
            Player.inventory[button1Index] = Player.inventory[button2Index];
            Player.inventory[button2Index] = temp;
        }
        RefreshInventoryUI();
    }

    public void DropSelectedItem(){
        if(isButton1Clicked){
            float xPos = Random.Range(0.0f,1.0f);
            float yPos = Mathf.Sqrt(1-Mathf.Pow(xPos,2));
            int minus = Random.Range(0,2);
            if(minus == 0){
                xPos = xPos * -1;
            }
            minus = Random.Range(0,2);
            if(minus == 0){
                yPos = yPos * -1;
            }
            ItemWorld.SpawnItemWorld(new Vector3(player.position.x + xPos, player.position.y + yPos), new Item { itemType = Player.inventory[button1Index].GetItemType()});
            Player.inventory[button1Index] = null;
            RefreshInventoryUI();
            isButton1Clicked = false;
        }
    }
}
