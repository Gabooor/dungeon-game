using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Az ellenfelek spawnolásáért felel
    // Első belépéskor elkezdi az ellenfeleket spawnolni
    public GameObject skeletonArcherPrefab;
    public GameObject skeletonWarriorPrefab;
    public GameObject enemyList;

    public GameObject towerRandomPrefab;
    public GameObject towerSpiralPrefab;

    public int enemySpawnAmount;

    public int enemyCount;
    public int towerCount;

    public GameObject player;

    public Room currentRoom;

    public void Start(){
        enemySpawnAmount = 16;
        enemyCount = 0;
        towerCount = 0;
    }

    public void Update(){
        foreach(Room room in DungeonGenerator.Rooms){
            Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.y);
            if((pos.x >= room.location.x) && (pos.x <= room.location.x + room.width) && (pos.y >= room.location.y) && (pos.y <= room.location.y + room.height)){
                room.isInRoom = true;
            }
            else{
                room.isInRoom = false;
            }
            if(room.isInRoom){
                currentRoom = room;
                if(room.firstEnter){
                    Vector3Int end = new Vector3Int(room.location.x + room.width, room.location.y + room.height, 0);
                    if(room.style != Room.Style.starter){
                        if(room.isTowerRoom){
                            StartCoroutine(SpawnTowers(room));
                        }
                        else{
                            room.enemyCount = enemySpawnAmount;
                            StartCoroutine(SpawnWaves(room));
                        }
                    }
                    room.firstEnter = false;
                }
            }
        }


    }

    IEnumerator SpawnWaves(Room room) {
        List<int> quadrons = new List<int>();
        yield return new WaitForSeconds(1);
        for(int j = 0; j < enemySpawnAmount; j++) {
                GameObject obj;
                int randEnemy = Random.Range(0,2);
                switch(randEnemy){
                    default:
                    case 0: obj = Instantiate(skeletonArcherPrefab, enemyList.transform); break;
                    case 1: obj = Instantiate(skeletonWarriorPrefab, enemyList.transform); break;
                }
                //GameObject obj = Instantiate(skeletonArcherPrefab, enemyList.transform);
                Enemy enemy = obj.transform.GetComponent<Enemy>();
                enemy.index = enemyCount;
                enemy.setRoomBoundaries(room.location.x,room.location.y, (room.location.x + room.width), (room.location.y + room.height));
                enemy.adherentRoom = room;
                //room.enemyCount++;
                bool goodQuadron = false;
                while(goodQuadron == false){
                    int quadron = -1;
                    Vector2 pos = new Vector2(Random.Range(room.location.x+1,room.location.x+room.width-1),Random.Range(room.location.y+1, room.location.y+room.height-1));
                    if(pos.x > room.location.x + room.width/2){
                        if(pos.y > room.location.y + room.height/2){
                            quadron = 2;
                        }
                        else{
                            quadron = 3;
                        }
                    }
                    else{
                        if(pos.y > room.location.y + room.height/2){
                            quadron = 1;
                        }
                        else{
                            quadron = 4;
                        }
                    }
                    //Debug.Log(quadron);
                    if(!quadrons.Contains(quadron)){
                        quadrons.Add(quadron);
                        //Debug.Log(quadron);
                        enemy.transform.position = new Vector2(pos.x, pos.y);
                        goodQuadron = true;
                   }
                }
                if(quadrons.Count == 4){
                    quadrons.Clear();
                }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnTowers(Room room){
        yield return new WaitForSeconds(2);
        
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

            switch(i%4){
                case 0: tower.transform.position = new Vector2(room.location.x + 1.5f, room.location.y + 1.5f); break; // Bottom left
                case 1: tower.transform.position = new Vector2(room.location.x + room.width - 1.5f, room.location.y + 1.5f); break; // Bottom right
                case 2: tower.transform.position = new Vector2(room.location.x + 1.5f, room.location.y + room.height - 1.5f); break; // Top left
                case 3: tower.transform.position = new Vector2(room.location.x + room.width - 1.5f, room.location.y + room.height - 1.5f); break; // Top right
            }
        }
        
    }
}