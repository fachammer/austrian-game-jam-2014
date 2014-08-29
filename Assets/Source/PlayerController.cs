using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public float speed = 10f;

	public float timeBetweenShots = 500.0f;

	//private Player player;
	private float lastTimeShot;

	private bool turnLeft = false;

	void Start()
    {
		//player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
		lastTimeShot = Time.time;
	}

	void Update()
    {

		Vector2 velocity = rigidbody2D.velocity;

		if(Input.GetKey(KeyCode.Space) && Time.time - lastTimeShot > timeBetweenShots){
			lastTimeShot = Time.time;
			//player.SendMessage("Shoot");
		}

		if (Input.GetAxis ("Horizontal") > 0) {
			if (turnLeft) {
				turnLeft = false;
				transform.localScale = new Vector3(1,1,1);
			}
			velocity.x += speed;
		} else if (Input.GetAxis("Horizontal") < 0) {
			if (!turnLeft) {
				turnLeft = true;
				transform.localScale = new Vector3(-1,1,1);
			}
			velocity.x -= speed;
		}

		rigidbody2D.velocity = velocity;

	}
}



