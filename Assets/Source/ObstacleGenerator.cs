using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {

    public GameObject obstacle;

    private Transform end;
    private List<int> xPosUsed;
    private int xPos;

	void Start () 
    {
        xPosUsed = new List<int>();
        end = transform.parent.FindChild("END");
        int count = Random.Range(1, 4);

        for (int i = 0; i < count; i++)
        {
            do {
                xPos = Random.Range((int)transform.parent.transform.position.x, (int)(end.transform.position.x));
            } while (xPosUsed.Contains(xPos));

            xPosUsed.Add(xPos);

            Instantiate(obstacle, new Vector2(xPos, 1), Quaternion.identity);
        }
    }
	
}
