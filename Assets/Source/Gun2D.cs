using UnityEngine;
using System.Collections;

public class Gun2D : MonoBehaviour {

	public float fireRate = 3;
	private float delay;

	void Start () {
	
	}
	
	void Update () {
	
		transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(Input.mousePosition.x - Screen.width/2 - transform.position.x, Input.mousePosition.y - Screen.height/2 - transform.position.y)*Mathf.Rad2Deg);

	}
}
