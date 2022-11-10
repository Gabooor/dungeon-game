using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Random referenciák
    protected Transform player;
    protected Rigidbody2D rb;

    // Mozgás
    protected Vector2 movement;
    protected Vector2 target = new Vector2(0,0);
    protected Vector2 dir;
    protected float speed = 3f;
    protected float movementTimer;

    // Indexelés
    public int index;
    public int enemyRoomIndex;
    public Room adherentRoom;

    // Mozgási területük
    protected int minX, minY, maxX, maxY;

    // Mozgáshoz státuszok
    protected enum State{
        lookingForTarget,
        headingToTarget,
        arrivedToTarget,
        waitingForTarget
    }

    protected State state;

    // Élet
    public int health;
    public bool isAlive;

    public void Start(){
        // Játékos és Rigidbody beállítás
        player = GameObject.Find("Player").transform;
        rb = transform.GetComponent<Rigidbody2D>();
        // Debug.Log("helo");

        // Mozgási idő nullázás
        movementTimer = 0;

        // Státuszok beállítása
        state = State.lookingForTarget;

        health = 30;
        isAlive = true;
    }

    void FixedUpdate(){
        // Debug.Log(minX);
        // A célpont választás óta eltelt idő
        movementTimer += 1f * Time.deltaTime;
        if(state == State.lookingForTarget){
            StartCoroutine(SetNewTarget());
            state = State.waitingForTarget;
        }
        if(state == State.headingToTarget){
            if(movementTimer > 10f){
                StartCoroutine(SetNewTarget());
                state = State.waitingForTarget;
            }
            else{
                dir = target - new Vector2(transform.position.x, transform.position.y);
                dir.Normalize();
                movement = dir;
                moveCharacter(movement);
                if(Vector2.Distance(target, new Vector2(transform.position.x, transform.position.y)) < 0.5){
                    state = State.lookingForTarget;
                }
            }
        // }
        // if(fs == FiringState.readyToFire){
        //     StartCoroutine(Shoot());
        //     fs = FiringState.waitingForCooldown;
        }
    }

    IEnumerator SetNewTarget(){
        yield return new WaitForSeconds(Random.Range(1, 2.5f));
        target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        while(Vector2.Distance(target, new Vector2(transform.position.x, transform.position.y)) > 12){
            target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        state = State.headingToTarget;
        movementTimer = 0;
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
    //         // Debug.Log(transform.position.x + ":" + transform.position.y + "|" + player.position.x + ":" + player.position.y + "|" + Vector2.Distance(new Vector2(transform.position.x, transform.position.y),new Vector2(player.position.x, player.position.y)));
    //     float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.position.x, player.position.y));
    //         // Debug.Log("Old distance: " + distance);
    //     // 2. Leosztani a kívánt távolsággal, multiplier = distance / wantedDistance
    //     float wantedDistance = 10.0f;
    //     float multiplier = distance / wantedDistance;
    //         // Debug.Log("távolság / kívánt (3) = " + multiplier);
    //     // 3. (playerPos.x - transform.pos.x) / wantedDistance
    //     float xMod = (player.position.x - transform.position.x) / multiplier;
    //         // Debug.Log("xMod = " + xMod);
    //     // 4. (playerPos.y - transform.pos.y) / wantedDistance
    //     float yMod = (player.position.y - transform.position.y) / multiplier;
    //         // Debug.Log("yMod = " + yMod);
    //     // 5. targetPos.x = transform.pos.x + xMod
    //     Vector2 targetPos;
    //     targetPos.x = transform.position.x + xMod;
    //     // 6. targetPos.y = transform.pos.y + yMod
    //     targetPos.y = transform.position.y + yMod;
    //         // Debug.Log("Új pozíció: " + targetPos);

    //     float newDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPos.x, targetPos.y));
    //         Debug.Log(newDistance);
    //     rb.AddForce(new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
    //     // rb.AddForce(new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y) * 0.4f, ForceMode2D.Impulse);
    //     fs = FiringState.readyToFire;
    // }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public void setRoomBoundaries(int MinX, int MinY, int MaxX, int MaxY){
        // Debug.Log(MinX + ":" + MinY + ":" + MaxX + ":" + MaxY);
        this.minX = MinX;
        this.minY = MinY;
        this.maxX = MaxX;
        this.maxY = MaxY;
        // Debug.Log(minX + " " + minY + " " + maxX + " " + maxY);
    }
    
    public void Die(){
        this.adherentRoom.enemyCount--;
        if(this.adherentRoom.enemyCount == 0){
            RoomManager.OpenDoors(this.adherentRoom);
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damage){
        if(damage >= health){
            isAlive = false;
            Die();
        }
        health -= damage;
    }
}