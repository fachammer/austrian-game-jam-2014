using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun2D : MonoBehaviour {

	public float fireRate = 3;
	private float delay;

	public Transform bullet;

	public int ammo;
	public Text ammoDisplay;

	public float minRotation = 0, maxRotation = 360;

	void Start () {
	
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

		float newRotation = -Mathf.Atan2(Input.mousePosition.x - Screen.width/2 - transform.position.x, Input.mousePosition.y - Screen.height/2 - transform.position.y)*Mathf.Rad2Deg + 90;
		if (newRotation > minRotation-90 && newRotation < maxRotation-90) {
			transform.localEulerAngles = new Vector3(0,0,newRotation);
		}

		if (Input.GetMouseButton (0)) {
			TryFire ();
		}

	}
}
