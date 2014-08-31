using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {

    public GameObject obstacle;
    public int obstacleSpawnDistance = 5;
    public GameObject netz;

    private GameObject player;
    private Transform end;
    private List<int> xPosUsed;
    private int xPos;

	void Start () 
    {
        player = GameObject.Find("Player");
        xPosUsed = new List<int>();
        end = transform.parent.FindChild("END");
        int count = Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            do {
                xPos = Random.Range((int)transform.parent.transform.position.x, (int)(end.transform.position.x));
            } while (xPosUsed.Contains(xPos) || (player.transform.position.x < xPos + obstacleSpawnDistance && player.transform.position.x > xPos - obstacleSpawnDistance));

            xPosUsed.Add(xPos);

            Instantiate(obstacle, new Vector2(xPos, 1), Quaternion.identity);
        }

        count = Random.Range(1, 2);

        for (int i = 0; i < count; i++) {
            do {
                xPos = Random.Range((int)transform.parent.transform.position.x, (int)(end.transform.position.x));
            } while (xPosUsed.Contains(xPos) || (player.transform.position.x < xPos + obstacleSpawnDistance && player.transform.position.x > xPos - obstacleSpawnDistance));

            xPosUsed.Add(xPos);

            Instantiate(netz, new Vector2(xPos, 5), Quaternion.identity);
        }
    }
	
}
