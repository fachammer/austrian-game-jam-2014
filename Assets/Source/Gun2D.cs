using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun2D : MonoBehaviour {

	public Transform shootPoint;

	public float fireRate = 3;
	private float delay;
	
	public Transform muzzleFire;
	public Transform impact;

	public int ammo = 3;
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
	
		GameObject go = Instantiate (muzzleFire, shootPoint.position, shootPoint.rotation) as GameObject;
//		go.transform.parent = this.transform;

		RaycastHit2D hit = Physics2D.Raycast (new Vector2(shootPoint.position.x, shootPoint.position.y), new Vector2(shootPoint.right.x, shootPoint.right.y));

		if (hit.collider.gameObject.layer == 8) {

			Instantiate(impact, hit.point, Quaternion.identity);

		}

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
			#if UNITY_STANDALONE
			xAxis = Input.mousePosition.x - Screen.width/2 - transform.position.x;
			yAxis = Input.mousePosition.y - Screen.height/2 - transform.position.y;
			#endif
			#if UNITY_ANDROID
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
				xAxis = Input.GetTouch(0).deltaPosition;.x - Screen.width/2 - transform.position.x;
				yAxis = Input.GetTouch(0).deltaPosition;.y - Screen.height/2 - transform.position.y;
			}
			#endif
		}

		if (!playerController.facingRight) {
			xAxis = -xAxis;
			yAxis = -yAxis;
		}

		if (useGamepad) {
			angle = Mathf.Atan2(xAxis,yAxis) * Mathf.Rad2Deg +90;
		} else {
			angle = -Mathf.Atan2(xAxis,yAxis) * Mathf.Rad2Deg -90;
		}

//		if (angle > minRotation-90 && angle < maxRotation-90) {
			if (playerController.facingRight) {
				transform.localEulerAngles = new Vector3(0,0,-angle);
			} else {
				transform.localEulerAngles = new Vector3(0,0,angle);
			}
//		}

		if (Input.GetButton("Fire1") || Input.GetAxis("FireJoystick") < -0.1) {
			TryFire ();
		}

	}
}
