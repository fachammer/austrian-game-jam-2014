using UnityEngine;
using System.Collections;

public class IntroTextMover : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.back * Time.deltaTime);
		transform.Translate(Vector3.up * Time.deltaTime/10, Space.World);
	}
}
