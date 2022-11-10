using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    // Lövéshez státuszok
    protected enum FiringState{
        waitingForCooldown,
        readyToFire
    }
    
    protected FiringState fs = FiringState.readyToFire;
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;

    void Update(){
        if(player.position.x >= minX && player.position.x <= maxX && player.position.y >= minY && player.position.y <= maxY){
            if(fs == FiringState.readyToFire){
                StartCoroutine(Shoot());
                fs = FiringState.waitingForCooldown;
            }
        }
    }

    IEnumerator Shoot(){
        yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.tag = "EnemyBullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 lookDir = new Vector2(player.position.x, player.position.y) - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        bullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle), Space.Self);

        // 1. Lekérni a távolságot
            // Debug.Log(transform.position.x + ":" + transform.position.y + "|" + player.position.x + ":" + player.position.y + "|" + Vector2.Distance(new Vector2(transform.position.x, transform.position.y),new Vector2(player.position.x, player.position.y)));
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));
            // Debug.Log("Old distance: " + distance);
        // 2. Leosztani a kívánt távolsággal, multiplier = distance / wantedDistance
        float wantedDistance = 4.0f;
        float multiplier = distance / wantedDistance;
            // Debug.Log("távolság / kívánt (3) = " + multiplier);
        // 3. (playerPos.x - transform.pos.x) / wantedDistance
        float xMod = (player.position.x - transform.position.x) / multiplier;
            // Debug.Log("xMod = " + xMod);
        // 4. (playerPos.y - transform.pos.y) / wantedDistance
        float yMod = (player.position.y - transform.position.y) / multiplier;
            // Debug.Log("yMod = " + yMod);
        // 5. targetPos.x = transform.pos.x + xMod
        Vector2 targetPos;
        targetPos.x = transform.position.x + xMod;
        // 6. targetPos.y = transform.pos.y + yMod
        targetPos.y = transform.position.y + yMod;
            // Debug.Log("Új pozíció: " + targetPos);

        float newDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPos.x, targetPos.y));
            // Debug.Log(newDistance);
        rb.AddForce(new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
        // rb.AddForce(new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
        fs = FiringState.readyToFire;
    }
}
