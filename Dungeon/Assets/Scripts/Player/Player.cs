using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 8f;
    public Rigidbody2D rb;
    public Vector2 movement;

    public static int health;

    public static int enemyCount;
    public static int towerCount;

    public static bool isAlive;

    public Transform firePoint;
    public GameObject bulletPrefab;
    private float bulletSpeed;
    private float shootingCooldown;
    private float originalShootingCooldown = 1f;
    public static bool isShooting;
    public float fireRate;
    public bool autoFire;

    private Inventory inventory;

    void Awake(){
        inventory = new Inventory();
    }

    void Start(){
        health = 100;
        
        isAlive = true;

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

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position + (direction * speed * Time.deltaTime));
    }

    public void TakeDamage(int damage){
        if(damage >= health){
            Player.isAlive = false;
            Debug.Log("You died");
        }
        health -= damage;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        bullet.transform.tag = "PlayerBullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}