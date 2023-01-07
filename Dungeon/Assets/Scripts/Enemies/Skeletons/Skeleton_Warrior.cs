using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Warrior : Enemy
{
    // Mozgás
    protected Vector2 movement;
    protected Vector2 target;
    protected Vector2 dir;
    protected float speed = 2.0f;
    protected float movementWaitingTimer = 0;
    protected float movementTimer = 0;
    
    // Mozgáshoz státuszok
    protected enum State{
        headingToTarget,
        arrivedToTarget
    }

    protected State state = State.arrivedToTarget;

    // Lövéshez státuszok
    protected enum FiringState{
        waitingForCooldown,
        readyToFire
    }
    
    protected FiringState fs = FiringState.readyToFire;
    
    //public Transform firePoint;
    //public GameObject bulletPrefab;
    public float bulletForce = 10f;

    void Update(){
        // if(player.position.x >= minX && player.position.x <= maxX && player.position.y >= minY && player.position.y <= maxY){
        //     if(fs == FiringState.readyToFire){
        //         StartCoroutine(Shoot());
        //         fs = FiringState.waitingForCooldown;
        //     }
        // }
    }

    void FixedUpdate(){
        target = new Vector2(player.position.x, player.position.y);
        // A célpont választás óta eltelt idő
        if(state == State.arrivedToTarget){
            movementWaitingTimer += 1f * Time.deltaTime;
        }
        if(movementWaitingTimer > 2.0f){
            state = State.headingToTarget;
        }
        if(state == State.headingToTarget){
            movementTimer += 1f * Time.deltaTime;
            if(movementTimer > 3.0f){
                movementWaitingTimer = 0;
                state = State.arrivedToTarget;
                movementTimer = 0;
            }
            dir = target - new Vector2(transform.position.x, transform.position.y);
            dir.Normalize();
            movement = dir;
            moveCharacter(movement);
        }
        // if(state == State.headingToTarget){
        //     if(movementTimer > 10f){
        //         StartCoroutine(SetNewTarget());
        //         state = State.waitingForTarget;
        //     }
        //     else{
        //         dir = target - new Vector2(transform.position.x, transform.position.y);
        //         dir.Normalize();
        //         movement = dir;
        //         moveCharacter(movement);
        //         if(Vector2.Distance(target, new Vector2(transform.position.x, transform.position.y)) < 0.5){
        //             state = State.arrivedToTarget;
        //         }
        //     }
        // }
    }

    // IEnumerator SetNewTarget(){
    //     yield return new WaitForSeconds(Random.Range(1, 2.5f));
    //     target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //     while(Vector2.Distance(target, new Vector2(transform.position.x, transform.position.y)) > 12){
    //         target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //     }
    //     state = State.headingToTarget;
    //     movementTimer = 0;
    // }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    // IEnumerator Shoot(){
    //     yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
    //     GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //     bullet.transform.tag = "EnemyBullet";
    //     Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    //     Vector2 lookDir = new Vector2(player.position.x, player.position.y) - new Vector2(transform.position.x, transform.position.y);
    //     float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
    //     bullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle), Space.Self);

    //     // 1. Lekérni a távolságot
    //     float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));
    //     // 2. Leosztani a kívánt távolsággal, multiplier = distance / wantedDistance
    //     float wantedDistance = 4.0f;
    //     float multiplier = distance / wantedDistance;
    //     // 3. (playerPos.x - transform.pos.x) / wantedDistance
    //     float xMod = (player.position.x - transform.position.x) / multiplier;
    //     // 4. (playerPos.y - transform.pos.y) / wantedDistance
    //     float yMod = (player.position.y - transform.position.y) / multiplier;
    //     // 5. targetPos.x = transform.pos.x + xMod
    //     Vector2 targetPos;
    //     targetPos.x = transform.position.x + xMod;
    //     // 6. targetPos.y = transform.pos.y + yMod
    //     targetPos.y = transform.position.y + yMod;
        
    //     float newDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPos.x, targetPos.y));
    //     rb.AddForce(new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
    //     fs = FiringState.readyToFire;
    // }
}
