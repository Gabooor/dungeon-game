using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Sword,

        HealthPotion,
        ManaPotion,

        Helmet,
        Chestplate,
        Leggings,
        Boots,

        Amulet,

        Ring
    }

    public ItemType itemType;

    public Sprite GetSprite(){
        switch(itemType){
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.ManaPotion: return ItemAssets.Instance.manaPotionSprite;

            case ItemType.Helmet: return ItemAssets.Instance.helmetSprite;
            case ItemType.Chestplate: return ItemAssets.Instance.chestplateSprite;
            case ItemType.Leggings: return ItemAssets.Instance.leggingsSprite;
            case ItemType.Boots: return ItemAssets.Instance.bootsSprite;
            
            case ItemType.Amulet: return ItemAssets.Instance.amuletSprite;
            case ItemType.Ring: return ItemAssets.Instance.ringSprite;
        }
    }

    public ItemType GetItemType(){
        return itemType;
    }

    public bool isWeapon()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: 
                return true;
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Helmet: 
            case ItemType.Chestplate: 
            case ItemType.Leggings: 
            case ItemType.Boots: 
            case ItemType.Amulet:
            case ItemType.Ring:
                return false;
        }
    }

    public bool isHelmet(){
        switch (itemType)
        {
            default:
            case ItemType.Helmet: 
                return true;
            case ItemType.Chestplate: 
            case ItemType.Leggings: 
            case ItemType.Boots: 
            case ItemType.Sword: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Amulet:
            case ItemType.Ring:
                return false;
        }
    }

    public bool isChestplate(){
        switch (itemType)
        {
            default:
            case ItemType.Chestplate: 
                return true;
            case ItemType.Helmet: 
            case ItemType.Leggings: 
            case ItemType.Boots: 
            case ItemType.Sword: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Amulet:
            case ItemType.Ring:
                return false;
        }
    }

    public bool isLeggings(){
        switch (itemType)
        {
            default:
            case ItemType.Leggings: 
                return true;
            case ItemType.Helmet: 
            case ItemType.Chestplate: 
            case ItemType.Boots: 
            case ItemType.Sword: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Amulet:
            case ItemType.Ring:
                return false;
        }
    }
    
    public bool isBoots(){
        switch (itemType)
        {
            default:
            case ItemType.Boots: 
                return true;
            case ItemType.Helmet: 
            case ItemType.Chestplate: 
            case ItemType.Leggings: 
            case ItemType.Sword: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Amulet:
            case ItemType.Ring:
                return false;
        }
    }
    
    public bool isAmulet(){
        switch (itemType)
        {
            default:
            case ItemType.Amulet:
                return true;
            case ItemType.Helmet: 
            case ItemType.Chestplate: 
            case ItemType.Leggings: 
            case ItemType.Boots: 
            case ItemType.Sword: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Ring:
                return false;
        }
    }
    
    public bool isRing(){
        switch (itemType)
        {
            default:
            case ItemType.Ring:
                return true;
            case ItemType.Helmet: 
            case ItemType.Chestplate: 
            case ItemType.Leggings: 
            case ItemType.Boots: 
            case ItemType.Sword: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Amulet:
                return false;
        }
    }

    public bool isArmor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Helmet: 
            case ItemType.Chestplate: 
            case ItemType.Leggings: 
            case ItemType.Boots: 
                return true;
            case ItemType.Sword: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.Amulet:
            case ItemType.Ring:
                return false;
        }
    }


    public GameObject GetBullet()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordBullet;  
        }
    }

    public int GetHPHealing()
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return 50;
        }
    }

    public int GetManaHealing()
    {
        switch (itemType)
        {
            default:
            case ItemType.ManaPotion: return 50;
        }
    }
    
    public int GetDamage()
    {
        switch (itemType)
        {
            default:
            //case ItemType.Sword: return (int)Mathf.Round(0.02f * (Mathf.Pow(level + 1, 3)) + 0.04f * (Mathf.Pow(level + 1, 2)) + level + 20);
            case ItemType.Sword: return 25;
        }
    }
    
    public int GetArmor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Helmet: return 2;
            case ItemType.Chestplate: return 5;
            case ItemType.Leggings: return 3;
            case ItemType.Boots: return 2;
        }
    }
}
