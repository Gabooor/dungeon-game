using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public static bool isMapOpen;
    public Camera mapCamera;
    public GameObject Map;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isMapOpen = false;
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            if(!isMapOpen){
                isMapOpen = true;
                Map.SetActive(true);
            }
            else{
                isMapOpen = false;
                Map.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if(isMapOpen){
            Vector3 cameraPos = new Vector3(Input.GetAxis("Horizontal") / 2, Input.GetAxis("Vertical") / 2, -10);
            mapCamera.transform.position = new Vector3(mapCamera.transform.position.x + cameraPos.x, mapCamera.transform.position.y + cameraPos.y, -10);
        }
    }
}
