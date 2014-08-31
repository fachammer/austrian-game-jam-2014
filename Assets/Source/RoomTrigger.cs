using UnityEngine;
using System.Collections;

public class RoomTrigger : MonoBehaviour {

    public GameObject tilePrefab, stairsDown, stairsUp;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (player.transform.position.x >= transform.position.x - 0.2f)
        {
            if (Random.Range(0, 5) == 0)
            {
                GameObject newRoom = (GameObject)Instantiate(stairsDown, transform.parent.FindChild("END").transform.position, Quaternion.identity);
            }
            else if (Random.Range(0, 5) == 1)
            {
                GameObject newRoom = (GameObject)Instantiate(stairsUp, transform.parent.FindChild("END").transform.position, Quaternion.identity);
            }
            else
            {
                GameObject newRoom = (GameObject)Instantiate(tilePrefab, transform.parent.FindChild("END").transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }
}
