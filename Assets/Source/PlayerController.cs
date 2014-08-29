using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float timeBetweenShots = 500.0f;

	//private Player player;
	private float lastTimeShot;

	void Start()
    {
		//player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
		lastTimeShot = Time.time;
	}

	void Update()
    {
		if(Input.GetKey(KeyCode.Space) && Time.time - lastTimeShot > timeBetweenShots){
			lastTimeShot = Time.time;
			//player.SendMessage("Shoot");
		}
	}
}



