using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyList;

    public GameObject towerRandomPrefab;
    public GameObject towerSpiralPrefab;

    // public List<Enemy> EnemyList;
    // public List<Tower> TowerList;

    public int enemyCount;
    public int towerCount;

    public GameObject player;

    public Room currentRoom;

    public void Start(){
        // EnemyList = new List<Enemy>();
        // TowerList = new List<Tower>();

        enemyCount = 0;
        towerCount = 0;

    }

    public void Update(){
        foreach(Room room in DungeonGenerator.Rooms){
            Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.y);
            if((pos.x >= room.location.x) && (pos.x <= room.location.x + room.width) && (pos.y >= room.location.y) && (pos.y <= room.location.y + room.height)){
                room.isInRoom = true;
                // Debug.Log(pos.x + "|" + room.location.x);
            }
            else{
                room.isInRoom = false;
            }
            if(room.isInRoom){
                currentRoom = room;
                if(room.firstEnter){
                    Vector3Int end = new Vector3Int(room.location.x + room.width, room.location.y + room.height, 0);
                    // Debug.Log(room.location + "" + end);
                    //Debug.Log(room.roomIndex + ":" + room);
                    //EnemySpawner.SpawnEnemy(new Vector2(room.location.x, room.location.y));
                    //room.SpawnSpawningPlatforms(room.width, room.height);
                    if(room.style != Room.Style.starter){
                        StartCoroutine(SpawnEnemies(new Vector2(room.location.x, room.location.y), room.width, room.height, room));
                    }
                    room.firstEnter = false;
                }
                //Debug.Log(room.roomIndex + ":" + room);
            }
            //Vector3Int topRightCorner = new Vector3Int(room.location.x + width - 1, room.location.y + height - 1, 0);
            //Debug.Log(room + ": " + room.roomIndex + "|" + room.location + "-" + topRightCorner);
        }

    }

    IEnumerator SpawnEnemies(Vector2 Location, int width, int height, Room room){
        yield return new WaitForSeconds(2);
        
        if(room.isTowerRoom){
            for(int i = 0; i < 4; i++){
                GameObject obj;
                int area = room.width * room.height;
                if(area < 196){
                    obj = Instantiate(towerRandomPrefab, enemyList.transform);
                }
                else{
                    int ranTower = Random.Range(0,2);
                        if(ranTower == 0){
                            obj = Instantiate(towerSpiralPrefab, enemyList.transform);
                        }
                        else{
                            obj = Instantiate(towerRandomPrefab, enemyList.transform);
                        }
                }
                Tower tower = obj.transform.GetComponent<Tower>();
                tower.adherentRoom = room;

                //TowerList.Add(tower);
                switch(i%4){
                    case 0: tower.transform.position = new Vector2(Location.x + 1.5f, Location.y + 1.5f); break; // Bottom left
                    case 1: tower.transform.position = new Vector2(Location.x + width - 1.5f, Location.y + 1.5f); break; // Bottom right
                    case 2: tower.transform.position = new Vector2(Location.x + 1.5f, Location.y + height - 1.5f); break; // Top left
                    case 3: tower.transform.position = new Vector2(Location.x + width - 1.5f, Location.y + height - 1.5f); break; // Top right
                }
                //towerCount++;

                // Debug.Log(tower.adherentRoom);
                // enemy.index = enemyCount;
                // tower.setRoomBoundaries((int) Location.x, (int) Location.y, ((int) Location.x + width), ((int) Location.y + height));
                // Debug.Log((int) Location.x + "|" + (int) Location.y + "|" + ((int) Location.x + width) + "|" + ((int) Location.y + height));
                // enemy.enemyRoomIndex = currentRoom.roomIndex;
                
                // TowerList.Add(tower);
                // switch(i%4){
                //     case 0: TowerList[towerCount].transform.position = new Vector2(Location.x + 1.5f, Location.y + 1.5f); break; // Bottom left
                //     case 1: TowerList[towerCount].transform.position = new Vector2(Location.x + width - 1.5f, Location.y + 1.5f); break; // Bottom right
                //     case 2: TowerList[towerCount].transform.position = new Vector2(Location.x + 1.5f, Location.y + height - 1.5f); break; // Top left
                //     case 3: TowerList[towerCount].transform.position = new Vector2(Location.x + width - 1.5f, Location.y + height - 1.5f); break; // Top right
                // }
                // towerCount++;
            }
        }
        else{
            for(int i = 0; i < 4; i++){
                GameObject obj = Instantiate(enemyPrefab, enemyList.transform);
                Enemy enemy = obj.transform.GetComponent<Enemy>();
                enemy.index = enemyCount;
                enemy.setRoomBoundaries((int) Location.x, (int) Location.y, ((int) Location.x + width), ((int) Location.y + height));
                // Debug.Log((int) Location.x + "|" + (int) Location.y + "|" + ((int) Location.x + width) + "|" + ((int) Location.y + height));
                //enemy.enemyRoomIndex = currentRoom.roomIndex;
                enemy.adherentRoom = room;
                room.enemyCount++;
                // Debug.Log(room.enemyCount);
                switch(i%4){
                    case 0: enemy.transform.position = new Vector2(Location.x + 1.5f, Location.y + 1.5f); break; // Bottom left
                    case 1: enemy.transform.position = new Vector2(Location.x + width - 1.5f, Location.y + 1.5f); break; // Bottom right
                    case 2: enemy.transform.position = new Vector2(Location.x + 1.5f, Location.y + height - 1.5f); break; // Top left
                    case 3: enemy.transform.position = new Vector2(Location.x + width - 1.5f, Location.y + height - 1.5f); break; // Top right
                }
                // enemyCount++;
                // EnemyList.Add(enemy);
                // switch(i%4){
                //     case 0: EnemyList[enemyCount].transform.position = new Vector2(Location.x + 1.5f, Location.y + 1.5f); break; // Bottom left
                //     case 1: EnemyList[enemyCount].transform.position = new Vector2(Location.x + width - 1.5f, Location.y + 1.5f); break; // Bottom right
                //     case 2: EnemyList[enemyCount].transform.position = new Vector2(Location.x + 1.5f, Location.y + height - 1.5f); break; // Top left
                //     case 3: EnemyList[enemyCount].transform.position = new Vector2(Location.x + width - 1.5f, Location.y + height - 1.5f); break; // Top right
                // }
                // enemyCount++;
            }

            // for(int i = 0; i < EnemyList.Count; i++){
            //     Debug.Log(EnemyList[i].enemyRoomIndex);
            // }

            // for(int i = 0; i < room.enemies.Length; i++){
            //     Debug.Log("Room #" + room.roomIndex + ": " + room.enemies[i].index);
            // }
        }
        }
}