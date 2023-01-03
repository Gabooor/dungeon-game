using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // --- Inventory ---
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    // --- Movement ---
    public float speed = 8f;
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
    private float originalShootingCooldown = 1f;
    public static bool isShooting;
    public float fireRate;
    public bool autoFire;

    void Start(){

        // Setting up inventory
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);


        // Spawning items
        float w = (float) DungeonGenerator.Rooms[0].width;
        float h = (float) DungeonGenerator.Rooms[0].height;
        Debug.Log(w);

        for(int i = 0; i < 75; i++){
            float randomW = Random.Range(0.0f,w) + 0.5f;
            float randomH = Random.Range(0.0f,h) + 0.5f;
            //Debug.Log(randomW + " " + randomH);
            
        ItemWorld.SpawnItemWorld(new Vector3(randomW,randomH), new Item { itemType = Item.ItemType.HealthPotion, amount = 1});
        }



        // Setting up health
        health = 100;
        isAlive = true;

        // Basic values for shooting
        shootingCooldown = 0f;
        fireRate = 3f;
        bulletSpeed = 12f;
        autoFire = false;
    }

    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        bullet.transform.tag = "PlayerBullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collider){
        // Picking up items
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null){
            Item[] itemList = inventory.GetItemList();
            for(int i = 0; i < inventory.maxItems; i++){
                if(itemList[i] == null){
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                    break;
                }
            }
            /*if(inventory.GetItemList().Count != inventory.maxItems){
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }*/
        }
    }
}