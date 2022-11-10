using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RightRoom: Room {

    public RightRoom(Room ParentRoom, int RoomIndex, int Width, int Height, Vector3Int Location){
        this.roomIndex = RoomIndex;
        this.parentRoom = ParentRoom;
        // this.parentRoomIndex = ParentRoomIndex;

        this.style = Style.right;

        this.width = Width;
        this.height = Height;

        this.location = new Vector3Int(Location.x, Location.y - Random.Range(1,height-1), 0);
        
        int random = 0;//Random.Range(0,4);
        if(random == 0){
            this.isTowerRoom = true;
        }

        int doors = Random.Range(0,9);
        if(doors >= 1 && doors <= 4){
            availableDoor1 = Random.Range(0,3);
            this.noDoors = 1;
        }
        if(doors >= 5){
            availableDoor1 = Random.Range(0,3);
            availableDoor2 = Random.Range(0,3);
            this.noDoors = 2;
            while(availableDoor1 == availableDoor2){
                availableDoor2 = Random.Range(0,3);
            }
        }

        int overlaps = 0;
        int testingColor = Random.Range(0,10);
        while(this.overlapping == Overlap.notTested){
            for(int i = 0; i < width + 2; i++){
                for(int j = 0; j < height + 2; j++){
                    if(DungeonGenerator.dungeon.GetTile<Tile>(new Vector3Int(location.x + i - 1, location.y + j - 1, 0)) != null) overlaps++;
                }
            }
            if(overlaps > 0){
                for(int i = 0; i < width; i++){
                    for(int j = 0; j < height; j++){
                        DungeonGenerator.test.SetTile(new Vector3Int(location.x + i, location.y + j, 0), DungeonGenerator.tiles[testingColor]);
                    }
                }
                this.location.y += 1;
                this.width -= 1;
                this.height -= 2;
                if(width < 6 || height < 6 || (Location.y < this.location.y+1 || Location.y > this.location.y+(height-2))){
                    width = 0;
                    height = 0;
                    isDeleted = true;
                    this.overlapping = Overlap.tested;
                }
            }
            else{
                this.overlapping = Overlap.tested;
            }
            overlaps = 0;
        }
        if(!isDeleted){
        // Debug.Log("RightRoom: " + this.roomIndex + ", parent room: " + this.parentRoom.roomIndex);
            this.doorTop = new Vector3Int(location.x + Random.Range(1,width-1), location.y + (height-1), 0);
            this.doorRight = new Vector3Int(location.x + (width-1), location.y + Random.Range(1,height-1), 0);
            this.doorBottom = new Vector3Int(location.x + Random.Range(1,width-1), location.y, 0);
            this.doorLeft = Location;
            DungeonGenerator.rooms++;
            DrawRightRoom();
            if(availableDoor1 == 0 || availableDoor2 == 0) TopRoom.GenerateTopRoom(this, DungeonGenerator.rooms, new Vector3Int(doorTop.x, doorTop.y + 2, 0));
            if(availableDoor1 == 1 || availableDoor2 == 1) GenerateRightRoom(this, DungeonGenerator.rooms, new Vector3Int(doorRight.x + 2, doorRight.y, 0));
            if(availableDoor1 == 2 || availableDoor2 == 2) BottomRoom.GenerateBottomRoom(this, DungeonGenerator.rooms, new Vector3Int(doorBottom.x, doorBottom.y - 2, 0));
            for(int i = 0; i < width + 2; i++){
                for(int j = 0; j < height + 2; j++){
                    if(DungeonGenerator.dungeon.GetTile<Tile>(new Vector3Int(location.x + i - 1, location.y + j - 1, 0)) == null){
                        DungeonGenerator.dungeon.SetTile(new Vector3Int(location.x + i - 1, location.y + j - 1, 0), DungeonGenerator.tiles[0]);
                        DungeonGenerator.walls.SetTile(new Vector3Int(location.x + i - 1, location.y + j - 1, 0), DungeonGenerator.barricade);
                    };
                }
            }
            // SpawnSpawningPlatforms(this.width, this.height);
        }
    }

    //Szoba kirajzolása
    public void DrawRightRoom(){        
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
        // DungeonGenerator.dungeon.SetTile(new Vector3Int(doorLeft.x,doorLeft.y,0), DungeonGenerator.tiles[this.color]);
        // DungeonGenerator.dungeon.SetTile(new Vector3Int(doorLeft.x-1,doorLeft.y,0), DungeonGenerator.tiles[this.color]);
        // DungeonGenerator.dungeon.SetTile(new Vector3Int(doorLeft.x-2,doorLeft.y,0), DungeonGenerator.tiles[this.color]);
        
        // foreach(Room room in DungeonGenerator.RoomsSorted){
        //     if(room.roomIndex == this.parentRoomIndex){
                if(this.parentRoom.isCleared){
                    for(int i = -1; i < 2; i++){
                        DungeonGenerator.dungeon.SetTile(new Vector3Int(doorLeft.x,doorLeft.y + i,0), DungeonGenerator.tiles[Random.Range(1,9)]);
                        DungeonGenerator.dungeon.SetTile(new Vector3Int(doorLeft.x-1,doorLeft.y + i,0), DungeonGenerator.tiles[Random.Range(1,9)]);
                        DungeonGenerator.dungeon.SetTile(new Vector3Int(doorLeft.x-2,doorLeft.y + i,0), DungeonGenerator.tiles[Random.Range(1,9)]);
                    } 
                    // foreach(Room room in DungeonGenerator.RoomsSorted){
                    //     Debug.Log(room + "|" + room.roomIndex + "|" + room.parentRoomIndex);
                    }
        //         }
        //     }
        // }
        
    }

    public static void GenerateRightRoom(Room ParentRoom, int RoomIndex, Vector3Int Location){
        int Width = Random.Range(10,18);
        int Height = Random.Range(10,14); 
        // RightRoom rightRoom = new RightRoom(RoomIndex+1, Width, Height, new Vector3Int(Location.x, Location.y, 0));
        DungeonGenerator.Rooms.Add(new RightRoom(ParentRoom, RoomIndex, Width, Height, new Vector3Int(Location.x, Location.y, 0)));
    }
}
