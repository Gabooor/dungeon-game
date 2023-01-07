using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake(){
        Instance = this;
    }

    public Transform pfItemWorld;

    [Header("Weapons")]
    public Sprite swordSprite;

    [Header("Bullets")]
    public GameObject swordBullet;

    [Header("Potions")]
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    
    [Header("Armor")]
    public Sprite helmetSprite;
    public Sprite chestplateSprite;
    public Sprite leggingsSprite;
    public Sprite bootsSprite;
    
    [Header("Jewelry")]
    public Sprite amuletSprite;
    public Sprite ringSprite;
}
