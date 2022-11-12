using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StarterRoom : Room {

    public StarterRoom(int RoomIndex, int Width, int Height, Vector3Int Location){

        this.roomIndex = RoomIndex;
        this.width = Width;
        this.height = Height;
        this.location = Location;
        this.isCleared = true;

        this.missingDoor = Random.Range(0,5);

        this.doorTop = new Vector3Int(location.x + Random.Range(1,width-1), location.y + (height-1), 0);
        this.doorRight = new Vector3Int(location.x + (width-1), location.y + Random.Range(1,height-1), 0);
        this.doorBottom = new Vector3Int(location.x + Random.Range(1,width-1), location.y, 0);
        this.doorLeft = new Vector3Int(location.x, location.y + Random.Range(1,height-1), 0);
        DungeonGenerator.rooms++;
        DrawRoom();
        if(missingDoor != 0) TopRoom.GenerateTopRoom(this, DungeonGenerator.rooms, new Vector3Int(doorTop.x, doorTop.y + 2, 0));
        if(missingDoor != 1) RightRoom.GenerateRightRoom(this, DungeonGenerator.rooms, new Vector3Int(doorRight.x + 2, doorRight.y, 0));
        if(missingDoor != 2) BottomRoom.GenerateBottomRoom(this, DungeonGenerator.rooms, new Vector3Int(doorBottom.x, doorBottom.y - 2, 0));
        if(missingDoor != 3) LeftRoom.GenerateLeftRoom(this, DungeonGenerator.rooms, new Vector3Int(doorLeft.x - 2, doorLeft.y, 0));
        for(int i = 0; i < width + 2; i++){
            for(int j = 0; j < height + 2; j++){
                if(DungeonGenerator.dungeon.GetTile<Tile>(new Vector3Int(location.x + i - 1, location.y + j - 1, 0)) == null){
                    DungeonGenerator.dungeon.SetTile(new Vector3Int(location.x + i - 1, location.y + j - 1, 0), DungeonGenerator.tiles[0]);
                    DungeonGenerator.walls.SetTile(new Vector3Int(location.x + i - 1, location.y + j - 1, 0), DungeonGenerator.barricade);
                };
            }
        }
    }

    //Szoba kirajzolása
    public void DrawRoom(){
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                int random = Random.Range(1,9);
                for(int k = 0; k < 2; k++){
                    if(random == 2 || random == 3){
                        random = Random.Range(1,9);
                    }
                }
                DungeonGenerator.dungeon.SetTile(new Vector3Int(location.x + i, location.y + j, 0), DungeonGenerator.tiles[random]);
            }
        }
    }
}
