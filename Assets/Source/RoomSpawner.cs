using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomSpawner : MonoBehaviour {
    
    public GameObject roomTile;

    void Start()
    {
        GameObject start = GameObject.Find("START");
        Instantiate(roomTile, start.transform.position, Quaternion.identity);
    }
}
