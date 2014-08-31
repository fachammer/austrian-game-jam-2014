using UnityEngine;
using System.Collections;

public class RoomTrigger : MonoBehaviour {

    public GameObject tilePrefab;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (player.transform.position.x >= transform.position.x - 0.2f)
        {
            GameObject newRoom = (GameObject) Instantiate(tilePrefab, transform.parent.FindChild("END").transform.position, Quaternion.identity);
            gameObject.SetActive( false );
        }
    }
}
