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
        if (player.transform.position.x >= transform.position.x - 0.5f && player.transform.position.x <= transform.position.x + 0.5f)
        {
            Instantiate(tilePrefab, transform.parent.FindChild("END").transform.position, Quaternion.identity);
            transform.gameObject.SetActive( false );
        }
    }
}
