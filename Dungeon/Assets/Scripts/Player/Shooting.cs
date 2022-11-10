using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public bool autoFire;

    private float bulletForce = 7f;

    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        autoFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            autoFire = !autoFire;
        }
        if(autoFire){
            Shoot();
            cooldown = 0.15f;
        }
        if (Input.GetMouseButton(0) && cooldown == 0)
        {
            Shoot();
            cooldown = 0.15f;
        }
        if (cooldown > 0)
        {
            cooldown -= 1 * Time.deltaTime;
        }
        if (cooldown < 0) cooldown = 0;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}