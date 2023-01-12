using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // --- Inventory ---
    public static Item[] inventory;
    public static int inventorySpace;
    
    public static Item helmet;
    public static Item chestplate;
    public static Item leggings;
    public static Item boots;
    public static Item amulet;
    public static Item ring;
    public static Item weapon;

    public int armor;

    // --- Movement ---
    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;

    // --- Health ---
    public static int health;
    public static bool isAlive;

    // --- Shooting ---
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float bulletSpeed;
    private float shootingCooldown;
    private float originalShootingCooldown;
    public static bool isShooting;
    public float fireRate;
    public bool autoFire;

    void Awake(){
        // Setting up inventory
        inventorySpace = 35;
        inventory = new Item[inventorySpace];
        
        // Setting up armor
        helmet = new Item { itemType = Item.ItemType.Helmet, level = 1};
        chestplate = new Item { itemType = Item.ItemType.Chestplate, level = 2};
        leggings = new Item { itemType = Item.ItemType.Leggings, level = 3};
        boots = new Item { itemType = Item.ItemType.Boots, level = 4};
        amulet = new Item { itemType = Item.ItemType.Amulet, level = 5};
        ring = new Item { itemType = Item.ItemType.Ring, level = 6};
        weapon = new Item { itemType = Item.ItemType.Sword, level = 7};
    }

    void Start(){
        speed = 8f;

        // Spawning items
        float w = (float) DungeonGenerator.Rooms[0].width;
        float h = (float) DungeonGenerator.Rooms[0].height;

        for(int i = 0; i < 15; i++){
            float randomW = Random.Range(0.0f,w) + 0.5f;
            float randomH = Random.Range(0.0f,h) + 0.5f;
            //Debug.Log(randomW + " " + randomH);
            
            int typeNum = Random.Range(0,3);
            switch(typeNum){
                default:
                case(0): ItemWorld.SpawnItemWorld(new Vector3(randomW,randomH), new Item { itemType = Item.ItemType.HealthPotion}); break;
                case(1): ItemWorld.SpawnItemWorld(new Vector3(randomW,randomH), new Item { itemType = Item.ItemType.ManaPotion}); break;
                case(2): ItemWorld.SpawnItemWorld(new Vector3(randomW,randomH), new Item { itemType = Item.ItemType.Sword}); break;
            }
        //ItemWorld.SpawnItemWorld(new Vector3(randomW,randomH), new Item { itemType = Item.ItemType.HealthPotion, amount = 1});
        }
        
        // armor = 0;
        // if(helmet != null){
        //     armor += helmet.GetArmor();
        // }
        // if(chestplate != null){
        //     armor += chestplate.GetArmor();
        // }
        // if(leggings != null){
        //     armor += leggings.GetArmor();
        // }
        // if(boots != null){
        //     armor += boots.GetArmor();
        // }
        // Debug.Log(armor);

        // Setting up health
        health = 100;
        isAlive = true;

        // Basic values for shooting

        shootingCooldown = 0f;
        originalShootingCooldown = 1f;
        fireRate = 3f;
        bulletSpeed = 12f;
        autoFire = false;
    }

    void Update()
    {   
        if(!MapMovement.isMapOpen){
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else{
            movement = new Vector2(0,0);
        }
        if(Input.GetKeyDown(KeyCode.X)){
            autoFire = !autoFire;
        }
        if(autoFire && shootingCooldown == 0){
            Shoot();
            shootingCooldown = originalShootingCooldown / fireRate;
        }

        if(Input.GetMouseButton(0) && shootingCooldown == 0 && !autoFire){
            Shoot();
            shootingCooldown = originalShootingCooldown / fireRate;
        }

        if(shootingCooldown > 0){
            shootingCooldown -= 1 * Time.deltaTime;
        }
        if(shootingCooldown < 0) shootingCooldown = 0;
    }

    public void FixedUpdate()
    {
        moveCharacter(movement);
    }

    public void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position + (direction * speed * Time.deltaTime));
    }

    public void TakeDamage(int damage){
        // If damage > health, you die
        if(damage >= health){
            Player.isAlive = false;
            Debug.Log("You died");
        }
        // else you take damage
        health -= damage;
    }

    public void Shoot()
    {
        if(weapon == null){
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        bullet.GetComponent<Bullet>().damage = weapon.GetDamage();
        bullet.transform.tag = "PlayerBullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collider){
        // Picking up items
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null){
            for(int i = 0; i < inventorySpace; i++){
                if(inventory[i] == null){
                    inventory[i] = itemWorld.GetItem();
                    itemWorld.DestroySelf();
                    InventoryUI.RefreshInventoryUI();
                    break;
                }
            }
        }
        int items = 0;
        for(int i = 0; i < inventorySpace; i++){
            if(inventory[i] != null) items++;
        }
        //Debug.Log(items + " items");
    }
}