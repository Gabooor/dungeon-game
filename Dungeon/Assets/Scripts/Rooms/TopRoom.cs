using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TopRoom : Room {

    public TopRoom(Room ParentRoom, int RoomIndex, int Width, int Height, Vector3Int Location){
        this.roomIndex = RoomIndex;
        this.parentRoom = ParentRoom;

        this.style = Style.top;

        this.width = Width;
        this.height = Height;

        this.location = new Vector3Int(Location.x - Random.Range(1,width-1), Location.y, 0);
        
        int random = Random.Range(0,4);
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
                this.location.x += 1;
                this.width -= 2;
                this.height -= 1;
                if(width < 10|| height < 10 || (Location.x < this.location.x+1 || Location.x > this.location.x+(width-2))){
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
            this.doorTop = new Vector3Int(location.x + Random.Range(1,width-1), location.y + (height-1), 0);
            this.doorRight = new Vector3Int(location.x + (width-1), location.y + Random.Range(1,height-1), 0);
            this.doorBottom = Location;
            this.doorLeft = new Vector3Int(location.x, location.y + Random.Range(1,height-1), 0);
            DungeonGenerator.rooms++;
            DrawTopRoom();

            if(availableDoor1 == 0 || availableDoor2 == 0) GenerateTopRoom(this, DungeonGenerator.rooms, new Vector3Int(doorTop.x, doorTop.y + 2, 0));
            if(availableDoor1 == 1 || availableDoor2 == 1) RightRoom.GenerateRightRoom(this, DungeonGenerator.rooms, new Vector3Int(doorRight.x + 2, doorRight.y, 0));
            if(availableDoor1 == 2 || availableDoor2 == 2) LeftRoom.GenerateLeftRoom(this, DungeonGenerator.rooms, new Vector3Int(doorLeft.x - 2, doorLeft.y, 0));
            for(int i = 0; i < width + 2; i++){
                for(int j = 0; j < height + 2; j++){
                    if(DungeonGenerator.dungeon.GetTile<Tile>(new Vector3Int(location.x + i - 1, location.y + j - 1, 0)) == null){
                        DungeonGenerator.dungeon.SetTile(new Vector3Int(location.x + i - 1, location.y + j - 1, 0), DungeonGenerator.tiles[0]);
                        DungeonGenerator.walls.SetTile(new Vector3Int(location.x + i - 1, location.y + j - 1, 0), DungeonGenerator.barricade);
                    };
                }
            }
        }
    }

    //Szoba kirajzolása
    public void DrawTopRoom(){
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
        if(this.parentRoom.isCleared){
            for(int i = -1; i < 2; i++){
                DungeonGenerator.dungeon.SetTile(new Vector3Int(doorBottom.x + i, doorBottom.y, 0), DungeonGenerator.tiles[Random.Range(1,9)]);
                DungeonGenerator.dungeon.SetTile(new Vector3Int(doorBottom.x + i, doorBottom.y-1, 0), DungeonGenerator.tiles[Random.Range(1,9)]);
                DungeonGenerator.dungeon.SetTile(new Vector3Int(doorBottom.x + i, doorBottom.y-2, 0), DungeonGenerator.tiles[Random.Range(1,9)]);
            }
        }
    }

    public static void GenerateTopRoom(Room ParentRoom, int RoomIndex, Vector3Int Location){
        int Width = Random.Range(minSizeX,maxSizeX);
        int Height = Random.Range(minSizeY,maxSizeY); 
        DungeonGenerator.Rooms.Add(new TopRoom(ParentRoom, RoomIndex, Width, Height, new Vector3Int(Location.x, Location.y, 0)));
    }
}
