using UnityEngine;
using System.Collections;

public class IntroPlayerHOLYSHIT : MonoBehaviour {

	public AudioClip[] holyShit;

	// Use this for initialization
	void Start () {
			
		Invoke("movePlayer", 30.0f);
		Invoke ("shoutHOLYSHIT", 30.5f);

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Jump") || Input.GetKeyDown("space")) {
			Application.LoadLevel("rooms");
		}
		 
	}

	void movePlayer() {

		rigidbody2D.fixedAngle = false;
		rigidbody2D.gravityScale = 0f;
		rigidbody2D.AddTorque(80f);
		rigidbody2D.angularDrag = 0f;
		rigidbody2D.AddForce(Vector2.right * 800f);
		rigidbody2D.AddForce(Vector2.up * 25f);
	}

	void shoutHOLYSHIT() {

		AudioSource.PlayClipAtPoint(holyShit[0], new Vector3(0, 0, 0));

	}
}
