using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Transform player;
    public Text timerText;
    public Text stateText;
    public Text locationText;
    public Text healthText;

    // Update is called once per frame
    void Update()
    {
        healthText.text = Player.health.ToString();
        int rooms = 0;
        foreach(Room CurrentRoom in DungeonGenerator.RoomsSorted){
            if(CurrentRoom.isInRoom){
                rooms++;
                if(CurrentRoom.isTowerRoom && CurrentRoom.timer < 35.0f){
                    CurrentRoom.timer += 1 * Time.deltaTime;
                    timerText.text = CurrentRoom.timer.ToString();
                }
                else timerText.text = "";
                if(CurrentRoom.isTowerRoom && CurrentRoom.timer > 35.0f && !CurrentRoom.isCleared){
                    OpenDoors(CurrentRoom);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            foreach(Room CurrentRoom in DungeonGenerator.RoomsSorted){
                if(CurrentRoom.isInRoom){
                    OpenDoors(CurrentRoom);
                }
            }
        }
    }

    public static void OpenDoors(Room CurrentRoom){
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
                    }
                }
                CurrentRoom.isCleared = true;
        // }
    }
}
