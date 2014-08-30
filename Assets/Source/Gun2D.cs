using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun2D : MonoBehaviour {

	public float fireRate = 3;
	private float delay;

	public Transform bullet;

	public int ammo;
	public Text ammoDisplay;

	public Text debugDisplay;

	public float minRotation = 0, maxRotation = 360;

	public PlayerController playerController;

	public bool useGamepad;

	void Start () {
	
		useGamepad = PlayerPrefs.GetInt ("useGamepad", 0) != 0 ? true : false;

	}

	public void TryFire () {
		if (delay <= 0 && ammo > 0) {
			delay = 1/fireRate;
			ammo --;
			Fire ();
		}
	}

	public void Fire () {
	
//		Instantiate (bullet, transform.position, transform.rotation);
		Debug.Log ("Fire");

	}
	
	void Update () {

		ammoDisplay.text = "Ammo: "+ammo;
	
		if (delay > 0) {
			delay -= Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			ammo += 3;
		}

//		debugDisplay.text = "GamepadInput: "+Input.GetAxis("GamepadX")+" "+Input.GetAxis("GamepadY");

		float angle, xAxis, yAxis;

		if (useGamepad) {
			xAxis = Input.GetAxis ("GamepadX");
			yAxis = Input.GetAxis ("GamepadY");
		} else {
			xAxis = Input.mousePosition.x - Screen.width/2 - transform.position.x;
			yAxis = Input.mousePosition.y - Screen.height/2 - transform.position.y;
		}

		if (!playerController.facingRight) {
			xAxis = -xAxis;
			yAxis = -yAxis;
		}

		if (useGamepad) {
			angle = Mathf.Atan2(xAxis,yAxis) * Mathf.Rad2Deg +90;
		} else {
			angle = Mathf.Atan2(xAxis,yAxis) * Mathf.Rad2Deg -90;
		}

//		if (angle > minRotation-90 && angle < maxRotation-90) {
			if (playerController.facingRight) {
				transform.localEulerAngles = new Vector3(0,0,-angle);
			} else {
				transform.localEulerAngles = new Vector3(0,0,angle);
			}
//		}

		if (Input.GetMouseButton (0)) {
			TryFire ();
		}

	}
}
