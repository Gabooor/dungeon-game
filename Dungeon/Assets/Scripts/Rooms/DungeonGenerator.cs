using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public Tilemap refColors, refDungeon, refWalls, refTest;
    public static Tilemap colors, dungeon, walls, test;
    public static Tile[] tiles = new Tile[10];
    public Tile refBarricade;
    public static Tile barricade;
    public static int rooms = 0;
    public int roomCount; //in-game
    public static List<Room> Rooms = new List<Room>();
    public static List<Room> RoomsSorted = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {   
        colors = refColors;
        dungeon = refDungeon;
        walls = refWalls;
        test = refTest;

        barricade = refBarricade;

        //Tile-ok beállítása egy meglévő tilemap-ról
        int index = 0;
        for(int i = 0; i < 10; i++){
            tiles[index] = colors.GetTile<Tile>(new Vector3Int(i,0,0));
            test.SetTile(new Vector3Int(i,0,0), tiles[index]);
            index++;
        }

        //Kezdőszoba dimenziók megadása
        int width = Random.Range(6,14);
        int height = Random.Range(6,10);

        //Ha magasabb a szoba, mint a hossza, akkor megcseréli a dimenziókat
        if(width < height){
            int temp = height;
            height = width;
            width = temp;
        }
        
        //Kezdőszoba legenerálása
        // bool bigEnough = false;
        // int tries = 0;
        // List<StarterRoom> starterrooms = new List<StarterRoom>();
        // while(bigEnough == false){
        //     rooms = 0;
        //     tries++;
        //     starterrooms.Add(new StarterRoom(width, height, new Vector3Int(-3,-4,0)));
        //     if(rooms < roomCount){
        //         starterrooms.RemoveAt(0);
        //         dungeon.ClearAllTiles();
        //         walls.ClearAllTiles();
        //         test.ClearAllTiles();
        //     }
        //     else{
        //         bigEnough = true;
        //         Debug.Log("roomCount: " + rooms + " Tries: " + tries);
        //     }
        // }
        
        //StarterRoom starterRoom = new StarterRoom(width, height, new Vector3Int(-3,-4,0));

        Rooms.Add(new StarterRoom(0, width, height, new Vector3Int(0,0,0)));
        // foreach(Room room in Rooms){
        //     if(room.isDeleted == false){
        //         Debug.Log(room + ":" + room.roomIndex);
        //     }
        // }
        int sortIndex = 0;
        for(int i = 0; i < rooms; i++){
            foreach(Room room in Rooms){
                if(room.isDeleted == false && room.roomIndex == sortIndex){
                    RoomsSorted.Add(room);
                }
            }
            sortIndex++;
        }
        Rooms.Clear();
        foreach(Room room in RoomsSorted){
            Rooms.Add(room);
        }
        foreach(Room room in Rooms){
            Vector3Int topRightCorner = new Vector3Int(room.location.x + room.width - 1, room.location.y + room.height - 1, 0);
            //Debug.Log(room + ": " + room.roomIndex + "|" + room.location + "-" + topRightCorner);
        }
        // for(int i = 0; i < rooms; i++){
        //     Debug.Log(i+":"+Rooms[i]);
        // }
        // Debug.Log(Rooms[0]);
        
        //Debug.Log(rooms);

        // StarterRoom[] testRooms = new StarterRoom[100];
        // for(int i = 0; i < 100; i++){
        //     testRooms[i] = new StarterRoom(Random.Range(4,12), Random.Range(4,8), new Vector3Int(0,(i+1)*20,0));
        // }
    }
}
