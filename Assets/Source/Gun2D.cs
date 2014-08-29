using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun2D : MonoBehaviour {

	public float fireRate = 3;
	private float delay;

	public Transform bullet;

	public int ammo;
	public Text ammoDisplay;

	void Start () {
	
	}

	public void TryFire () {
		if (delay <= 0) {
			delay = 1/fireRate;
			Fire ();
		}
	}

	public void Fire () {
	
		Instantiate (bullet, transform.position, transform.rotation);

	}
	
	void Update () {
	
		if (delay > 0) {
			delay -= Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			ammo += 3;
		}

		transform.eulerAngles = new Vector3(0,0,-Mathf.Atan2(Input.mousePosition.x - Screen.width/2 - transform.position.x, Input.mousePosition.y - Screen.height/2 - transform.position.y)*Mathf.Rad2Deg + 90);

		if (Input.GetMouseButton (0)) {
			TryFire ();
		}

	}
}
