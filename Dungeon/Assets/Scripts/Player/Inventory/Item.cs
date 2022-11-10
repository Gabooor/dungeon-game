using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{
    public enum ItemType{
        MagicStaff,
        MagicWand,
        Sword,
        Bow,

        LeatherHelmet,
        ChainmailHelmet,
        PlateHelmet,

        LeatherChestplate,
        ChainmailChestplate,
        PlateChestplate,

        LeatherLeggings,
        ChainmailLeggings,
        PlateLeggings,

        IceAmulet,
        FireAmulet,
        LightningAmulet,

        RingOfDamage,
        RingOfHealth,
        RingOfDefense,
        RingOfLeveling,

        BookOfDexterity,
        BookOfLightning,
        BookOfFreezing,

        HeavyShield,
        MediumShield
    }

    public ItemType itemType;
    public int amount;
}
