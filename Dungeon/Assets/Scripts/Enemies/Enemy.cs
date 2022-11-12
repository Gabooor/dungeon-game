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
    protected float minX, minY, maxX, maxY;

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

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public void setRoomBoundaries(float MinX, float MinY, float MaxX, float MaxY){
        this.minX = MinX;
        this.minY = MinY;
        this.maxX = MaxX;
        this.maxY = MaxY;
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