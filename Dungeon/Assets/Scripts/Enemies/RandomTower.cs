using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTower : Tower
{

    // Update is called once per frame
    void Update()
    {
        if(adherentRoom.isCleared){
            Destroy(gameObject);
        }
        if(Player.isAlive){
            if(!adherentRoom.isCleared && adherentRoom.isInRoom){
                if(fs == FiringState.readyToFire){
                    StartCoroutine(Shoot());
                    fs = FiringState.waitingForCooldown;
                }
            }
        }
    }

    IEnumerator Shoot(){
        yield return new WaitForSeconds(Random.Range(0.6f, 1.2f));
        for(int i = 0; i < 7; i++){
        //yield return new WaitForSeconds(0.2f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.tag = "EnemyBullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        int random1 = Random.Range(0,2); // 0+ 1-
        int random2 = Random.Range(0,2); // 0+ 1-

        float wantedDistance2 = 6.0f;
        float xDistance;

        if(random1 == 0){
            xDistance = Random.Range(transform.position.x, transform.position.x + wantedDistance2);
        }
        else{
            xDistance = Random.Range(transform.position.x, transform.position.x - wantedDistance2);
        }

        Vector2 tempPos = new Vector2(xDistance, transform.position.y); // B PONT
        Vector2 targetPos2;

        if(random2 == 0){
            targetPos2 = new Vector2(xDistance, tempPos.y + Mathf.Sqrt((Mathf.Pow(wantedDistance2, 2)-Mathf.Pow(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), tempPos), 2))));
        }
        else{
            targetPos2 = new Vector2(xDistance, tempPos.y - Mathf.Sqrt((Mathf.Pow(wantedDistance2, 2)-Mathf.Pow(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), tempPos), 2))));
        }

        // Debug.Log(transform.position.x + "|" + transform.position.y + "  <>  " + targetPos2.x + "|" + targetPos2.y);

        Vector2 lookDir = new Vector2(targetPos2.x, targetPos2.y) - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        bullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle), Space.Self);


        // 1. Lekérni a távolságot
            // Debug.Log(transform.position.x + ":" + transform.position.y + "|" + player.position.x + ":" + player.position.y + "|" + Vector2.Distance(new Vector2(transform.position.x, transform.position.y),new Vector2(player.position.x, player.position.y)));
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));
            // Debug.Log("Old distance: " + distance);
        // 2. Leosztani a kívánt távolsággal, multiplier = distance / wantedDistance
        float wantedDistance = 13f;
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
        rb.AddForce(new Vector2(targetPos2.x - transform.position.x, targetPos2.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
        // rb.AddForce(new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
        fs = FiringState.readyToFire;
        }
    }
}
