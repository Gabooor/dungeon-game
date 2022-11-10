using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{

    public int width;
    public int height;
    public Vector3Int location;
    public bool isCleared = false;
    public bool isInRoom = false;
    public bool firstEnter = true;
    public bool isTowerRoom = false;

    public enum Style
    {
        top,
        right,
        bottom,
        left,
        starter,
    }

    public Style style = Style.starter;

    public enum Overlap
    {
        notTested,
        tested,
    }
    
    public Overlap overlapping = Overlap.notTested;

    public int color = 15;

    public Vector3Int doorTop;
    public Vector3Int doorRight;
    public Vector3Int doorBottom;
    public Vector3Int doorLeft;

    public int missingDoor;
    public int availableDoor1 = -1;
    public int availableDoor2 = -1;
    public int noDoors = 0;

    public bool isDeleted = false;

    public Room parentRoom;
    public int roomIndex;

    public Enemy[] enemies;
    public int enemyCount = 0;
    
    public float timer = 0.0f;
}
