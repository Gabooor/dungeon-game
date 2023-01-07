using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float timer = 0;
    //protected float aliveTime = 1.5f;
    // private int penetration = 0;
    // public static int maxPenetration = 0;
    public int damage;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1f * Time.deltaTime;
        if(timer > 3)
        {
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(transform.tag == "EnemyBullet"){
            if(col.transform.tag.Equals("Player")){
                Player player = col.transform.GetComponent<Player>();
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        if(transform.tag == "PlayerBullet"){
            if(col.transform.tag.Equals("Enemy")){
                Enemy enemy = col.transform.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                //Debug.Log(enemy.health);
                Destroy(gameObject);
            }
        }
        if(col.transform.tag.Equals("Wall")){
            Destroy(gameObject);
        }
        // if (col.transform.tag.Equals("Enemy"))
        // {
        //     Enemy enemy = col.transform.GetComponent<Enemy>();
        //     switch (Player.equippedItem.GetName())
        //     {
        //         default:
        //         case "Sword": enemy.takeDamage(Player.equippedItem.GetDamage(Player.swordLevel) + Player.damageBonus); break;
        //         case "Staff": enemy.takeDamage(Player.equippedItem.GetDamage(Player.staffLevel) + Player.damageBonus); break;
        //     }
        //     if(penetration < maxPenetration)
        //     {
        //         penetration++;
        //     }
        //     else
        //     {
        //         Destroy(gameObject);
        //     }
        // }
    }
}
