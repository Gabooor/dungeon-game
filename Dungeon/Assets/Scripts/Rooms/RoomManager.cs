using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    // A szobák kinyitásáért felel
    // Vagy az összes ellenfél meghalt, vagy túlélte a játékos a 35 másodpercet
    public Transform player;
    public Text timerText;
    public Text stateText;
    public Text locationText;
    public Text healthText;

    public GameObject chestPrefab;
    public Transform chestContainer;

    // Update is called once per frame
    void Update()
    {
        foreach(Room room in DungeonGenerator.Rooms){
            Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.y);
            if((pos.x >= room.location.x) && (pos.x <= room.location.x + room.width) && (pos.y >= room.location.y) && (pos.y <= room.location.y + room.height)){
                room.isInRoom = true;
            }
            else{
                room.isInRoom = false;
            }
        }
        foreach(Room CurrentRoom in DungeonGenerator.RoomsSorted){
            if(CurrentRoom.isInRoom){
                Debug.Log(CurrentRoom.roomIndex + ": " + CurrentRoom.location);
            }
        }

        healthText.text = Player.health.ToString();
        foreach(Room CurrentRoom in DungeonGenerator.RoomsSorted){
            if(CurrentRoom.isInRoom){
                if(CurrentRoom.isTowerRoom && CurrentRoom.timer > 0.0f){
                    CurrentRoom.timer -= 1 * Time.deltaTime;
                    timerText.text = CurrentRoom.timer.ToString();
                }
                else timerText.text = "";
                if(CurrentRoom.isTowerRoom && CurrentRoom.timer < 0.0f && !CurrentRoom.isCleared){
                    OpenDoors(CurrentRoom);
                    SpawnChest(CurrentRoom);
                }
                if(CurrentRoom.enemyCount == 0 && !CurrentRoom.isCleared && !CurrentRoom.isTowerRoom){
                    Debug.Log("asd");
                    OpenDoors(CurrentRoom);
                    SpawnChest(CurrentRoom);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            foreach(Room CurrentRoom in DungeonGenerator.RoomsSorted){
                if(CurrentRoom.isInRoom){
                    OpenDoors(CurrentRoom);
                    SpawnChest(CurrentRoom);
                }
            }
        }
    }

    public void OpenDoors(Room CurrentRoom){
        // foreach(Room CurrentRoom in DungeonGenerator.RoomsSorted){
                foreach(Room ChildRoom in DungeonGenerator.RoomsSorted){
                    if(ChildRoom.parentRoom == CurrentRoom){
                        switch(ChildRoom.style){
                            case Room.Style.starter: {
                                break;
                            }
                            case Room.Style.top: {
                                for(int i = -1; i < 2; i++){
                                    DungeonGenerator.dungeon.SetTile(new Vector3Int(ChildRoom.doorBottom.x + i, ChildRoom.doorBottom.y - 1, 0), DungeonGenerator.tiles[Random.Range(1,9)]);
                                    DungeonGenerator.walls.SetTile(new Vector3Int(ChildRoom.doorBottom.x + i, ChildRoom.doorBottom.y - 1, 0), null);
                                }
                                break;
                            }
                            case Room.Style.right: {
                                for(int i = -1; i < 2; i++){
                                    DungeonGenerator.dungeon.SetTile(new Vector3Int(ChildRoom.doorLeft.x - 1 ,ChildRoom.doorLeft.y + i,0), DungeonGenerator.tiles[Random.Range(1,9)]);
                                    DungeonGenerator.walls.SetTile(new Vector3Int(ChildRoom.doorLeft.x - 1 ,ChildRoom.doorLeft.y + i,0), null);
                                } 
                                break;
                            }
                            case Room.Style.bottom: {
                                for(int i = -1; i < 2; i++){
                                    DungeonGenerator.dungeon.SetTile(new Vector3Int(ChildRoom.doorTop.x + i, ChildRoom.doorTop.y + 1, 0), DungeonGenerator.tiles[Random.Range(1,9)]);
                                    DungeonGenerator.walls.SetTile(new Vector3Int(ChildRoom.doorTop.x + i, ChildRoom.doorTop.y + 1, 0), null);
                                }
                                break;
                            }
                            case Room.Style.left: {
                                for(int i = -1; i < 2; i++){
                                    DungeonGenerator.dungeon.SetTile(new Vector3Int(ChildRoom.doorRight.x + 1,ChildRoom.doorRight.y + i,0), DungeonGenerator.tiles[Random.Range(1,9)]);
                                    DungeonGenerator.walls.SetTile(new Vector3Int(ChildRoom.doorRight.x + 1,ChildRoom.doorRight.y + i,0), null);
                                }
                                break;
                            }
                        }
                        //SpawnChest(CurrentRoom);
                    }
                }
                CurrentRoom.isCleared = true;
        // }
    }

    public void SpawnChest(Room currentRoom){
        Debug.Log("Finished Room #" + currentRoom.roomIndex + ", location is" + currentRoom.location);
        GameObject chest = Instantiate(chestPrefab, chestContainer);
        chest.transform.position = new Vector3(currentRoom.location.x + (currentRoom.width/2), currentRoom.location.y + (currentRoom.height/2), 10);
    }
}
