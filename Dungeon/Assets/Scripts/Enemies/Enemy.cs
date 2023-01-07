using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Az ellenfél alap tulajdonságait tartalmazza
    // Szoba referencia, élet, halál, sebződés

    // Random referenciák
    protected Transform player;
    protected Rigidbody2D rb;

    // Mozgási területük
    protected float minX, minY, maxX, maxY;

    // Indexelés
    public int index;
    public int enemyRoomIndex;
    public Room adherentRoom;

    // Élet
    public int health;
    public bool isAlive;

    public void Start(){
        //Debug.Log(this.adherentRoom.enemyCount);
        // Játékos és Rigidbody beállítás
        player = GameObject.Find("Player").transform;
        rb = transform.GetComponent<Rigidbody2D>();

        health = 20;
        isAlive = true;
    }
    public void Die(){
        this.adherentRoom.enemyCount--;
        if(this.adherentRoom.enemyCount == 0){
            RoomManager.OpenDoors(this.adherentRoom);
        }
        Destroy(gameObject);
    }

    public void setRoomBoundaries(float MinX, float MinY, float MaxX, float MaxY){
        this.minX = MinX;
        this.minY = MinY;
        this.maxX = MaxX;
        this.maxY = MaxY;
    }
    
    public void TakeDamage(int damage){
        if(damage >= health){
            isAlive = false;
            Die();
        }
        health -= damage;
    }
}