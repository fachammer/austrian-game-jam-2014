using UnityEngine;
using System.Collections;

public class RoomSpawner : MonoBehaviour {

    public GameObject roomTile;

    void Start()
    {
        GameObject start = GameObject.Find("START");
        Instantiate(roomTile, start.transform.position, Quaternion.identity);
    }
}
