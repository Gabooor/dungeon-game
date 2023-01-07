using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralTower : Tower
{
    Vector2 lookDir = new Vector2(3,0);
    float lookAngle;
    public int damage = 20;

    // Update is called once per frame
    void Update()
    {
        if(adherentRoom.isCleared){
            Destroy(gameObject);
        }
        // Debug.Log(startingPoint);
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
        yield return new WaitForSeconds(0.2f);
        // lookDir = new Vector2(3,0);
        //lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 15f;
        //Debug.Log(lookAngle);
                //yield return new WaitForSeconds(0.2f);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Bullet>().damage = damage;
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
                // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 65f;
                firePoint.Rotate(new Vector3(0.0f, 0.0f, lookAngle), Space.Self);
                bullet.transform.Rotate(new Vector3(0.0f, 0.0f, lookAngle), Space.Self);

                // rb.AddForce(new Vector2(targetPos2.x - transform.position.x, targetPos2.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
                rb.AddForce(firePoint.transform.up * 0.6f, ForceMode2D.Impulse);
                fs = FiringState.readyToFire;

    }
}
