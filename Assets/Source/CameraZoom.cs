using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public enum Controlls {ScrollWhell, PlusMinus, Both}

	public Controlls controlls;

	public float min = 5, max = 50;

	private float current;

	void Start () {
	
		if (camera.isOrthoGraphic) {
			current = camera.orthographicSize;
		} else {
			current = -camera.transform.position.z;
		}

	}
	
	void Update () {
	
		if (camera.isOrthoGraphic) {
			camera.orthographicSize = current;
		} else {
			transform.position = new Vector3(transform.position.x, transform.position.y, -current);
		}

		if (controlls == Controlls.PlusMinus) {
			if (Input.GetKeyDown(KeyCode.Plus) && current < max) {
				current ++;
			} else if (Input.GetKeyDown(KeyCode.Minus) && current > min) {
				current --;
			}
		} else if (controlls == Controlls.ScrollWhell) {
			if (Input.GetAxis ("Mouse ScrollWheel") < 0 && current < max) {
				current ++;
			} else if (Input.GetAxis ("Mouse ScrollWheel") > 0 && current > min) {
				current --;
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Plus) && current < max) {
				current ++;
			} else if (Input.GetKeyDown(KeyCode.Minus) && current > min) {
				current --;
			}
			if (Input.GetAxis ("Mouse ScrollWheel") < 0 && current < max) {
				current ++;
			} else if (Input.GetAxis ("Mouse ScrollWheel") > 0 && current > min) {
				current --;
			}
		}

	}
}
